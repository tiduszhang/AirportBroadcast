using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.UI;
using AirportBroadcast.ActiveMQ.Dto;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Configuration;
using AirportBroadcast.Domain.activeMq;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;
using AirportBroadcast.Utility;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ
{
    public interface IReceiveJsonAppService : IApplicationService
    {
        List<AirShowDataDto> GetAllArr();

        List<AirShowDataDto> GetAllDep();

        List<CommAudioFileNameDto> GetAllAudioFiles(EntityDto<long> input);

        List<ReceiveJsonDto> GetAllRecivelogs();

        List<PlayAudioLogDto> GetAllPlaylogs();

        List<CommAudioFileNameDto> GetAllPlaying();

        List<AudioTurnPlateDto> GetAllTurnPlate();

        List<AudioCheckInDto> GetAllCheckIn();

        List<AudioGateDto> GetAllGate();

        List<AudioReasonDto> GetAllReason();

        Task HandPlay(HandPlayDto input);

        void HandPlayText(HandPlayTextDto input);

        string Test(string jsonStr);
    }
    [AbpAuthorize]
    public class ReceiveJsonAppService : AbpZeroTemplateAppServiceBase, IReceiveJsonAppService
    {
        private readonly IRepository<AirshowData, long> repository;
        private readonly IRepository<CommAudioFileName, long> _crepository;
        private readonly IRepository<ReceiveJson, long> _rcrepository;
        private readonly IRepository<AudioTurnPlate> _audioTurnPlateRepository;
        private readonly IRepository<AudioCheckIn> _checkinRepository;

        private readonly IRepository<AudioGate> _gateRepository;
        private readonly IRepository<AudioReason> _reasonRepository;

        private readonly IRepository<PlayAudioLog, long> _logRepository;

        private readonly ICommAudioTempleAppService _play;
        private readonly ActiveMQListener listener;
        private readonly IAppFolders appFolders;
        private readonly IRepository<TopPwrPort> topPwrRepository;
        private readonly IRepository<AudioDevice> deviceRepository;

        public ReceiveJsonAppService(IRepository<AirshowData, long> repository,
             IRepository<ReceiveJson, long> _rcrepository,
             IRepository<CommAudioFileName, long> _crepository,
             IRepository<AudioCheckIn> _checkinRepository,
             IRepository<AudioGate> _gateRepository,
             IRepository<AudioReason> _reasonRepository,
             IRepository<TopPwrPort> topPwrRepository,
             IRepository<PlayAudioLog, long> _logRepository,
             IRepository<AudioDevice> deviceRepository,
             ICommAudioTempleAppService _play,
             ActiveMQListener listener,
              IAppFolders appFolders,
             IRepository<AudioTurnPlate> _audioTurnPlateRepository)
        {
            this.repository = repository;
            this._crepository = _crepository;
            this._rcrepository = _rcrepository;
            this._audioTurnPlateRepository = _audioTurnPlateRepository;
            this._checkinRepository = _checkinRepository;
            this._gateRepository = _gateRepository;
            this._reasonRepository = _reasonRepository;
            this._logRepository = _logRepository;
            this._play = _play;
            this.listener = listener;
            this.appFolders = appFolders;
            this.topPwrRepository = topPwrRepository;
            this.deviceRepository = deviceRepository;
        }

        public List<AirShowDataDto> GetAllArr()
        {
            return repository.GetAll()
                 .Where(x => x.FlightDateTime >= DateTime.Today)
                 .Where(x => x.DeporArrCode == "J").ToList().MapTo<List<AirShowDataDto>>();
        }

        public List<AirShowDataDto> GetAllDep()
        {
            return repository.GetAll()
               .Where(x => x.FlightDateTime >= DateTime.Today)
               .Where(x => x.DeporArrCode == "C").ToList().MapTo<List<AirShowDataDto>>();
        }

        public List<CommAudioFileNameDto> GetAllAudioFiles(EntityDto<long> input)
        {
            var list = _crepository.GetAll().Where(x => x.AirshowDataId == input.Id && x.PlayStatus == PlayStatus.暂未播放).ToList();

            return list.MapTo<List<CommAudioFileNameDto>>(); ;
        }

        public List<ReceiveJsonDto> GetAllRecivelogs()
        {
            var list = _rcrepository.GetAll().Where(x => x.ReciveTime > DateTime.Today)
                .OrderByDescending(x => x.ReciveTime).ToList();

            return list.MapTo<List<ReceiveJsonDto>>(); ;
        }

        public List<PlayAudioLogDto> GetAllPlaylogs()
        {
            var list = _logRepository.GetAll()
                .Where(x => x.CreationTime > DateTime.Today)
                .OrderByDescending(x => x.CreationTime).ToList();
                    

            return list.MapTo<List<PlayAudioLogDto>>(); 
        }

        public List<CommAudioFileNameDto> GetAllPlaying()
        {
            var list = _crepository.GetAll()
               .Where(x => x.CreationTime > DateTime.Today)
               .Where(x => x.PlayStatus == PlayStatus.开始播放 || x.PlayStatus == PlayStatus.等待播放)
               .OrderByDescending(x => x.PlayStatus)
               .ThenByDescending(x => x.StartPlayTime)
               .ThenByDescending(x => x.WaitForPlay)
               .ToList();

            return list.MapTo<List<CommAudioFileNameDto>>(); ;
        }

        public List<AudioTurnPlateDto> GetAllTurnPlate()
        {
            return _audioTurnPlateRepository.GetAll().Where(x => x.LanguageId == 1).ToList().MapTo<List<AudioTurnPlateDto>>();
        }

        public List<AudioCheckInDto> GetAllCheckIn()
        {
            return _checkinRepository.GetAll().Where(x => x.LanguageId == 1).ToList().MapTo<List<AudioCheckInDto>>();
        }

        public List<AudioGateDto> GetAllGate()
        {
            return _gateRepository.GetAll().Where(x => x.LanguageId == 1).ToList().MapTo<List<AudioGateDto>>();
        }

        public List<AudioReasonDto> GetAllReason()
        {
            return _reasonRepository.GetAll().Where(x => x.LanguageId == 1).ToList().MapTo<List<AudioReasonDto>>();
        }

        public async Task HandPlay(HandPlayDto input)
        {
            var p = SettingManager.GetSettingValue(AppSettings.General.PlayWay);
            if (!string.IsNullOrEmpty(p) && p == "1")
            {
                throw new UserFriendlyException("已被其他人切换为【自动播放】，请刷新页面重新切换！");
            }

            StringBuilder logmsg = new StringBuilder();
            logmsg.Append("手动播放：航班");
            var entity = repository.FirstOrDefault(input.Aid);
            if (entity == null) throw new UserFriendlyException("未找到对应航班信息");
            logmsg.Append(entity.FlightNo2); 

            var oldGate = "";
            //值机：CKI ， CKOFF
            //登机：BOR_N ， URBOR ，BOR_G，READY
            //出港：DLY_C ， CAN_C 
            //进港：EARR，ARR，FBAG，DLY_J， CAN_J 
            switch (input.PlayCommand)
            {
                case "CKI":
                    logmsg.Append("开始办理乘机手续,值机柜台:");
                    logmsg.Append(input.CheckInCode);
                    entity.FlightCirculationStatus = "CKI";
                    entity.CheckinCounter = input.CheckInCode;
                    ; break;
                case "CKOFF":
                    logmsg.Append("结束办理乘机手续");
                    entity.FlightCirculationStatus = "CKO";
                    ; break; 
                case "BOR_N":
                    logmsg.Append("正常登机,登机口：");
                    logmsg.Append(input.GateCode);
                    entity.FlightCirculationStatus = "BOR";
                    entity.Gate = input.GateCode;
                    ; break;
                case "URBOR":
                    logmsg.Append("催促登机,登机口：");
                    logmsg.Append(input.GateCode);
                    entity.FlightCirculationStatus = "LBD";
                    entity.Gate = input.GateCode;
                    ; break;
                case "BOR_G":
                    logmsg.Append("更改登机口,新登机口：");
                    logmsg.Append(input.GateCode);
                    entity.FlightCirculationStatus = "BOR";
                    entity.Gate = input.GateCode;
                    oldGate = "9999";
                    ; break;
                case "READY":
                    logmsg.Append("登机结束");
                    entity.FlightCirculationStatus = "POK";
                    ; break;
                case "DLY_C":
                    logmsg.Append("出港延误，原因：");
                    logmsg.Append(input.ReasonCode);
                    entity.FlightCirculationStatus = "DLY";
                    entity.DeporArrCode = "C";
                    entity.FlightAbnormalReason = input.ReasonCode;
                    if (!string.IsNullOrEmpty(input.ArrOrDepTime))
                    {
                        logmsg.Append("，预计起飞时间：");
                        logmsg.Append(input.ArrOrDepTime);
                        entity.DepForecastTime = DateTime.Today.Add(TimeSpan.Parse(input.ArrOrDepTime));
                    }
                    else
                    {
                        entity.DepForecastTime = DateTime.MinValue;
                    }
                    break;

                case "CAN_C":
                    logmsg.Append("出港取消，原因：");
                    logmsg.Append(input.ReasonCode);
                    entity.FlightCirculationStatus = "CAN";
                    entity.DeporArrCode = "C";
                    entity.FlightAbnormalReason = input.ReasonCode;
                    ; break;
                case "EARR":
                    logmsg.Append("进港正常预告");
                    entity.FlightCirculationStatus = "EARR";
                    ; break;
                case "ARR":
                    logmsg.Append("进港航班到达");
                    entity.FlightCirculationStatus = "ARR";
                    break;
                case "FBAG":
                    logmsg.Append("进港行李提取，行李转盘：");
                    logmsg.Append(input.TurnPlateCode);
                    entity.FlightCirculationStatus = "FBAG";
                    entity.Carousel = input.TurnPlateCode;
                    break;
                case "DLY_J":
                    logmsg.Append("进港延误预告，原因：");
                    logmsg.Append(input.ReasonCode);
                    entity.FlightCirculationStatus = "DLY";
                    entity.DeporArrCode = "J";
                    entity.Carousel = input.ReasonCode;
                    if (!string.IsNullOrEmpty(input.ArrOrDepTime))
                    {
                        logmsg.Append("，预计到达时间：");
                        logmsg.Append(input.ArrOrDepTime);
                        entity.ArrForecastTime = DateTime.Today.Add(TimeSpan.Parse(input.ArrOrDepTime));
                    }
                    else
                    {
                        entity.ArrForecastTime = DateTime.MinValue;
                    }
                    break;
                case "CAN_J":
                    logmsg.Append("进港取消，原因：");
                    logmsg.Append(input.ReasonCode);
                    entity.FlightCirculationStatus = "CAN";
                    entity.DeporArrCode = "J";
                    entity.Carousel = input.ReasonCode;
                    if (!string.IsNullOrEmpty(input.ArrOrDepTime))
                    {
                        logmsg.Append("，预计到达时间：");
                        logmsg.Append(input.ArrOrDepTime);
                        entity.ArrForecastTime = DateTime.Today.AddDays(1).Add(TimeSpan.Parse(input.ArrOrDepTime));
                    }
                    else
                    {
                        entity.ArrForecastTime = DateTime.MinValue;
                    }
                    break;

            }
            _logRepository.Insert(new PlayAudioLog() { FileName = "---", Remark = logmsg.ToString() });
            repository.Update(entity);
            var dto = Mapper.Map<AirShowDataDto>(entity);
            if (dto.Gate != oldGate && !string.IsNullOrEmpty(oldGate))
            {
                dto.GateOld = oldGate;
            }
            _play.ClearPlayQuenue();
            await _play.PlayAudioTemple(dto, true);

        }


        public void HandPlayText(HandPlayTextDto input)
        {
            #region 生成文件名称
            var todayPath = DateTime.Today.ToString("yyyy-MM-dd");

            string root_path = Path.Combine(appFolders.AudioFolder, "Temp", todayPath);
            if (!Directory.Exists(root_path))
            {
                Directory.CreateDirectory(root_path);
            }

            string wavName =  "HandTxt_" + Guid.NewGuid().ToString().Replace("-", "") + ".wav";
            string outputFile = Path.Combine(root_path, wavName);
            #endregion

            #region 生成播放文件，根据次数循环，拼接在一个文件
            var playtext = "";
            if (input.PlayTimes < 1) return;
            for (int i = 0; i < input.PlayTimes; i++)
            {
                playtext = playtext + ",,,,," + input.PlayText;
            }
            VoiceBroadcastUtil.PlayTextToFile(playtext, outputFile);
            #endregion

            #region 组装需打开的电源控制器的端口
            var entitys = topPwrRepository.GetAll().Where(x => input.TopPortIds.Contains(x.Id)).ToList();

            string port = "00000000";
            entitys.ForEach(x =>
            {
                var index = int.Parse(x.Code);
                port = port.Substring(0, index - 1) + "1" + port.Substring(index);
            });
            #endregion

            #region 查询所有声卡 ，暂未用到
            var des = deviceRepository.GetAllList().Select(x => x.Name).ToList();
            #endregion

           


            _play.PlayText(outputFile, port, des);


            //  VoiceBroadcastUtil.PlayText(input.PlayText);
        }

        public string Test(string jsonStr)
        {
           var f =  fileToString(jsonStr);

            var list = JsonConvert.DeserializeObject<List<AirShowDataDto>>(f);
            list.ForEach(item =>
            {
                listener.DoMsg(JsonConvert.SerializeObject(item));
            });
            return "all succ";
        }

        public string fileToString(String filePath)
        {
            StringBuilder build = new StringBuilder();
            try
            {
                string line;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        build.Append(line);
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return build.ToString();
        }
    }
}
