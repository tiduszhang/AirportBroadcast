using Abp.Application.Services;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using AirportBroadcast.ActiveMQ.Dto;
using AirportBroadcast.AudioControl;
using AirportBroadcast.Chat;
using AirportBroadcast.Chat.Dto;
using AirportBroadcast.Configuration;
using AirportBroadcast.Configuration.Host.Dto;
using AirportBroadcast.Domain.activeMq;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;
using AirportBroadcast.PlayAudio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ
{
    public interface ICommAudioTempleAppService : IApplicationService
    {
        Task PlayAudioTemple(AirShowDataDto data, bool ForcePlay = false);

        List<string> CommAudioTemple(AirShowDataDto data);

        void NotifyPlaying(PlayStateDto input);

        void ClearPlayQuenue();

        void PlayText(string filePath, string topPorts, List<string> devices);

        void SetPlayWayAuto();

        void Test(string jsonStr);

    }

    public class CommAudioTempleAppService : AbpZeroTemplateAppServiceBase, ICommAudioTempleAppService
    {
        private readonly IRepository<AudioPlaySet> paySetRepository;
        private readonly IRepository<AudioPlaySetTemple> paySetTempleRepository;

        private readonly IRepository<AudioHour> _audioHourRepository;
        private readonly IRepository<AudioMinite> _audioMiniteRepository;
        private readonly IRepository<AudioDigit> _audioDigitRepository;
        private readonly IRepository<AudioCity> _audioCityRepository;
        private readonly IRepository<AudioAirLine> _audioAirLineRepository;
        private readonly IRepository<AudioGate> _audioGateRepository;
        private readonly IRepository<AudioCheckIn> _audioCheckInRepository;
        private readonly IRepository<AudioTurnPlate> _audioTurnPlateRepository;
        private readonly IRepository<AudioReason> _audioReasonRepository;
        private readonly IRepository<CommAudioFileName, long> _commAudioFileNameRepository;


        private readonly IWavCombine _wavCombine;
        private readonly IPlayAudioHub hub;


        public CommAudioTempleAppService(IRepository<AudioPlaySet> paySetRepository,
            IRepository<AudioPlaySetTemple> paySetTempleRepository,
            IRepository<AudioHour> _audioHourRepository,
            IRepository<AudioMinite> _audioMiniteRepository,
            IRepository<AudioDigit> _audioDigitRepository,
            IRepository<AudioCity> _audioCityRepository,
            IRepository<AudioAirLine> _audioAirLineRepository,
            IRepository<AudioGate> _audioGateRepository,
            IRepository<AudioCheckIn> _audioCheckInRepository,
            IRepository<AudioTurnPlate> _audioTurnPlateRepository,
            IRepository<AudioReason> _audioReasonRepository,
            IRepository<CommAudioFileName, long> _commAudioFileNameRepository,
             IPlayAudioHub hub,
            IWavCombine _wavCombine)
        {
            this.paySetRepository = paySetRepository;
            this.paySetTempleRepository = paySetTempleRepository;
            this._audioHourRepository = _audioHourRepository;
            this._audioMiniteRepository = _audioMiniteRepository;
            this._audioDigitRepository = _audioDigitRepository;
            this._audioCityRepository = _audioCityRepository;
            this._audioAirLineRepository = _audioAirLineRepository;
            this._audioGateRepository = _audioGateRepository;
            this._audioCheckInRepository = _audioCheckInRepository;
            this._audioTurnPlateRepository = _audioTurnPlateRepository;
            this._audioReasonRepository = _audioReasonRepository;
            this._wavCombine = _wavCombine;
            this._commAudioFileNameRepository = _commAudioFileNameRepository;
            this.hub = hub;



        }

        #region 清空播放列表
        public void ClearPlayQuenue()
        {
            _wavCombine.ClearPlayQuenue();
            var entitys = _commAudioFileNameRepository.GetAll().Where(x => x.PlayStatus == PlayStatus.等待播放 || x.PlayStatus == PlayStatus.开始播放).ToList();
            entitys.ForEach(x =>
            {
                x.PlayStatus = PlayStatus.暂未播放;
            });

        }

        #endregion

        #region 根据Mq接收到的数据，拼装模版
        public List<string> CommAudioTemple(AirShowDataDto data)
        {
            var entities = CommAudioTempleInner(data);
            if (entities != null && entities.Count > 0)
                return entities.Select(x => x.FileName).ToList();
            return new List<string>();
        }

        #endregion

        #region 根据Mq接收到的数据，播放
        public async Task PlayAudioTemple(AirShowDataDto data, bool ForcePlay = false)
        {
            #region 判断是否为自动广播
            var p = SettingManager.GetSettingValue(AppSettings.General.PlayWay);
            if (!string.IsNullOrEmpty(p) && p == "2" && !ForcePlay)
            {
                Logger.Debug("此时为人工播放！");
                return;
            }
            #endregion

            #region 拼接语音片
            var list = CommAudioTempleInner(data);
            if (list == null || list.Count == 0) return;
            #endregion

            var code = GetCode(data);
            var playSet = paySetRepository.FirstOrDefault(x => x.Code == code);
            if (playSet.AutoPlay || ForcePlay)
            {
                string devices = "";
                string port = "00000000";
                playSet.AudioDevices.ForEach(x =>
                {
                    devices = x.Name;
                });

                if (data.FlightType == "I") //国际航班
                {
                    playSet.EnTopPwrPorts.ForEach(x =>
                    {
                        var index = int.Parse(x.Code);
                        port = port.Substring(0, index - 1) + "1" + port.Substring(index);

                    });
                }
                else  //D 国内,Q 地区
                {
                    playSet.CnTopPwrPorts.ForEach(x =>
                    {
                        var index = int.Parse(x.Code);
                        port = port.Substring(0, index - 1) + "1" + port.Substring(index);

                    });
                }



                if (list != null && list.Count > 0)
                {
                    list.ForEach(x =>
                    {
                        x.PlayStatus = PlayStatus.等待播放;
                        x.PlayPort = port;
                        x.WaitForPlay = DateTime.Now;
                        if (ForcePlay)
                        {
                            _wavCombine.Art_Player(x.FileName, devices, port);
                        }
                        else
                        {
                            _wavCombine.Player(x.FileName, devices, port);
                        }

                    });
                }

            }
        }
        #endregion

        public void PlayText(string filePath, string topPorts, List<string> devices)
        {
            #region 自动切换为  手动播放

            SettingManager.ChangeSettingForApplication(AppSettings.General.PlayWay, "2");

            #endregion

            _wavCombine.Art_PlayerTxt(filePath, "", topPorts);

        }

        public void SetPlayWayAuto()
        {
            #region 切换为  自动播放

            SettingManager.ChangeSettingForApplication(AppSettings.General.PlayWay, "1");

            #endregion             

        }


        #region 通知正在播放的文件状态

        public void NotifyPlaying(PlayStateDto input)
        {
            var entity = _commAudioFileNameRepository.FirstOrDefault(x => x.FileName == input.FileName);
            if (entity == null)
            {
                Logger.ErrorFormat("数据库中未找到正在播放的文件：{0},状态：{1}(0开始播放,1播放结束)", input.FileName, input.PlayState);
            }

            if (input.PlayState == 0)
            {
                entity.StartPlayTime = DateTime.Now;
                entity.PlayStatus = PlayStatus.开始播放;
            }
            else if (input.PlayState == 1)
            {
                entity.EndPlayTime = DateTime.Now;
                entity.PlayStatus = PlayStatus.播放完成;
            }

        }

        #endregion

        #region 根据Mq接收到的数据，拼装模版
        private List<CommAudioFileName> CommAudioTempleInner(AirShowDataDto data)
        {
            var times = int.Parse(SettingManager.GetSettingValue(AppSettings.General.PlayTimes));
            var canPlayLanguagesStr = SettingManager.GetSettingValue(AppSettings.General.CanPlayLanguages);
            var canPlayLanguages = JsonConvert.DeserializeObject<List<ChooseLanguages>>(canPlayLanguagesStr);


            var result = new List<CommAudioFileName>();
            if (times <= 0 || canPlayLanguages == null || canPlayLanguages.Count == 0)
                return result;

            var code = GetCode(data);

            var playSet = paySetRepository.FirstOrDefault(x => x.Code == code);
            if (playSet == null) return result;
            var tps = paySetTempleRepository.GetAllIncluding(x => x.AudioTemplte).Where(x => x.PlayId == playSet.Id);

            var fullFileNameList = new List<string>();
            StringBuilder contextSb = new StringBuilder();

            for (int index = 0; index < times; index++)
            {
                contextSb.Clear();
                tps.OrderBy(x => x.Sort).ToList().ForEach(x =>
                {
                    var set = canPlayLanguages.FirstOrDefault(item => item.Id == x.AudioTemplte.LanguageId);
                    if (set != null && set.IsChecked)
                    {
                        contextSb.Append(x.AudioTemplte.AudioLanguage.Name);
                        contextSb.Append("：");
                        x.AudioTemplte.Details.OrderBy(xx => xx.Sort).ToList().ForEach(d =>
                        {
                            if (d.IsParamter)
                            {
                                var dtos = CommParamter(d.ParamterType.Value, d.AudioTemplte.LanguageId, data);
                                dtos.ForEach(dto =>
                                {
                                    fullFileNameList.Add(dto[0]);
                                    contextSb.Append(dto[1]);
                                });
                            }
                            else
                            {
                                fullFileNameList.Add(d.AudioConst.Path + d.AudioConst.FileName);
                                contextSb.Append(d.AudioConst.Content);
                            }
                        });
                    }
                });
            }
            var p = _wavCombine.Concatenate(fullFileNameList, data.DeporArrCode);

            var entity = new CommAudioFileName()
            {
                AirshowDataId = data.Id,
                FileName = p.FileName,
                FileTotalTime = (int)p.WavLength,
                Remark = contextSb.ToString(),
                PlayStatus = PlayStatus.暂未播放,
                TotalPlayTime = 0,
                CreationTime = DateTime.Now
            };

            entity = _commAudioFileNameRepository.Insert(entity);
            result.Add(entity);
            CurrentUnitOfWork.SaveChanges();



            return result;
        }

        #endregion

        #region 拼装变量
        private List<string[]> CommParamter(ParamterType paramterType, int LanguageId, AirShowDataDto data)
        {
            var result = new List<string[]>();

            switch (paramterType)
            {
                case ParamterType.Routeoid_航班主键:
                    result.AddRange(Digit(LanguageId, data.Routeoid));
                    break;
                case ParamterType.FlightDateTime_航班日期:
                    if (data.FlightDateTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.FlightDateTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.FlightDateTime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.ExecutionDateTime_航班执行日期:
                    if (data.ExecutionDateTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.ExecutionDateTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.ExecutionDateTime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.FlightNo2_航班号:
                    result.Add(AudioAirLine(LanguageId, data.FlightNo2.Substring(0, 2)));
                    result.AddRange(Digit(LanguageId, data.FlightNo2.Substring(2)));
                    break;
                case ParamterType.FlightNo3_三字码航班号:
                    result.Add(AudioAirLine(LanguageId, data.FlightNo3.Substring(0, 3)));
                    result.AddRange(Digit(LanguageId, data.FlightNo3.Substring(3)));
                    break;
                case ParamterType.AirlineCode2_航司二字码:
                    result.Add(AudioAirLine(LanguageId, data.AirlineCode2));
                    break;
                case ParamterType.AirlineCode3_航司三字码:
                    result.Add(AudioAirLine(LanguageId, data.AirlineCode3));
                    break;
                case ParamterType.FlightNum_航班号_除航司部分:
                    result.AddRange(Digit(LanguageId, data.FlightNum));
                    break;
                case ParamterType.FlightMssion_航班性质:
                    break;
                case ParamterType.FlightType_航班类型:
                    break;
                case ParamterType.DepFIV1NO3_出港经停1三字码:
                    result.Add(City(LanguageId, data.DepFIV1NO3));
                    break;
                case ParamterType.DepFIV1NO4_出港经停1四字码:
                    result.Add(City(LanguageId, data.DepFIV1NO4));
                    break;
                case ParamterType.DepFIV2NO3_出港经停2三字码:
                    result.Add(City(LanguageId, data.DepFIV2NO3));
                    break;
                case ParamterType.DepFIV2NO4_出港经停2四字码:
                    result.Add(City(LanguageId, data.DepFIV2NO4));
                    break;
                case ParamterType.ArrFIV1NO3_进港经停1三字码:
                    result.Add(City(LanguageId, data.ArrFIV1NO3));
                    break;
                case ParamterType.ArrFIV1NO4_进港经停1四字码:
                    result.Add(City(LanguageId, data.ArrFIV1NO4));
                    break;
                case ParamterType.ArrFIV2NO4_进港经停2四字码:
                    result.Add(City(LanguageId, data.ArrFIV2NO4));
                    break;
                case ParamterType.Fiv1No3_经停1三字码:
                    result.Add(City(LanguageId, data.Fiv1No3));
                    break;
                case ParamterType.Fiv1No4_经停1四字码:
                    result.Add(City(LanguageId, data.Fiv1No4));
                    break;
                case ParamterType.Fiv2No3_经停2三字码:
                    result.Add(City(LanguageId, data.Fiv2No3));
                    break;
                case ParamterType.Fiv2No4_经停2四字码:
                    result.Add(City(LanguageId, data.Fiv2No4));
                    break;
                case ParamterType.ForgNo3_起场三字码:
                    result.Add(City(LanguageId, data.ForgNo3));
                    break;
                case ParamterType.ForgNo4_起场四字码:
                    result.Add(City(LanguageId, data.ForgNo4));
                    break;
                case ParamterType.FestNo3_落场三字码:
                    result.Add(City(LanguageId, data.FestNo3));
                    break;
                case ParamterType.FestNo4_落场四字码:
                    result.Add(City(LanguageId, data.FestNo4));
                    break;
                case ParamterType.DepPlanTime_计划起飞时间:
                    if (data.DepPlanTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.DepPlanTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.DepPlanTime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.DepForecastTime_预计起飞时间:
                    if (data.DepForecastTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.DepForecastTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.DepForecastTime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.DepartTime_实际起飞时间:
                    if (data.DepartTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.DepartTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.DepartTime.Value.Minute.ToString().Trim()));
                    }

                    break;
                case ParamterType.ArrPlanTime_计划落地时间:
                    if (data.ArrPlanTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.ArrPlanTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.ArrPlanTime.Value.Minute.ToString().Trim()));

                    }

                    break;
                case ParamterType.ArrForecastTime_预计落地时间:
                    if (data.ArrForecastTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.ArrForecastTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.ArrForecastTime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.ArriveTime_实际落地时间:
                    if (data.ArriveTime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.ArriveTime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.ArriveTime.Value.Minute.ToString().Trim()));

                    }

                    break;
                case ParamterType.FlightStatus_航班状态:
                    break;
                case ParamterType.Gate_登机口:
                    result.Add(Gate(LanguageId, data.Gate));
                    break;
                case ParamterType.Gateopentime_登机口计划开启时间:
                    if (data.Gateopentime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.Gateopentime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.Gateopentime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.Gateclosetime_登机口计划关闭时间:
                    if (data.Gateclosetime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.Gateclosetime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.Gateclosetime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.Carousel_行李转盘:
                    result.Add(AudioTurnPlate(LanguageId, data.Carousel));
                    break;
                case ParamterType.CheckinLoad_值机岛:
                    break;
                case ParamterType.CheckinCounter_值机柜台:
                    result.Add(AudioCheckIn(LanguageId, data.CheckinCounter));
                    break;
                case ParamterType.CheckinTimeStart_值机开始时间:
                    if (data.CheckinTimeStart.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.CheckinTimeStart.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.CheckinTimeStart.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.CheckinTimeEnd_值机结束时间:
                    if (data.CheckinTimeEnd.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.CheckinTimeEnd.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.CheckinTimeEnd.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.FlightCirculationStatus_航班流转状态:
                    break;
                case ParamterType.FlightAbnormalReason_异常原因:
                    result.Add(AudioReason(LanguageId, data.FlightAbnormalReason));
                    break;
                case ParamterType.Freg_机号:
                    break;
                case ParamterType.DeporArrCode_进出港标记:
                    break;
                case ParamterType.LocalAirportCode_本场代码:
                    break;
                case ParamterType.Xlsjtime_行李上架时间:
                    if (data.Xlsjtime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.Xlsjtime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.Xlsjtime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.Xlxjtime_行李下架时间:
                    if (data.Xlxjtime.HasValue)
                    {
                        result.Add(Hour(LanguageId, data.Xlxjtime.Value.Hour.ToString().Trim()));
                        result.Add(Minite(LanguageId, data.Xlxjtime.Value.Minute.ToString().Trim()));
                    }
                    break;
                case ParamterType.Dlytype_延误类型:
                    break;
                case ParamterType.Dlytime_延误时间:
                    if (!string.IsNullOrEmpty(data.Dlytime))
                    {
                        result.Add(Hour(LanguageId, data.Dlytime.Substring(0, 2)));
                        result.Add(Minite(LanguageId, data.Dlytime.Substring(2, 2)));
                    }
                    break;
                case ParamterType.Counterno_柜台号码:
                    result.AddRange(Digit(LanguageId, data.Counterno));
                    break;
            }

            return result;
        }


        private string[] Hour(int LanguageId, string hour)
        {
            var entity = _audioHourRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == hour);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] Minite(int LanguageId, string min)
        {
            var minStr = min.PadLeft(2, '0');

            var entity = _audioMiniteRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == minStr);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private List<string[]> Digit(int LanguageId, string Digit)
        {
            var result = new List<string[]>();
            if (string.IsNullOrEmpty(Digit)) return result;

            for (int i = 0; i < Digit.Trim().Length; i++)
            {
                var oneDigit = Digit.Substring(i, 1);
                var entity = _audioDigitRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == oneDigit);
                if (entity != null)
                    result.Add(new string[] { entity.Path + entity.FileName, entity.Content });
            }
            return result;
        }

        private string[] City(int LanguageId, string city)
        {
            var entity = _audioCityRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == city);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] AudioAirLine(int LanguageId, string aircode)
        {
            var entity = _audioAirLineRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == aircode);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] Gate(int LanguageId, string gate)
        {
            var entity = _audioGateRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == gate);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] AudioCheckIn(int LanguageId, string code)
        {
            var entity = _audioCheckInRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == code);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] AudioTurnPlate(int LanguageId, string code)
        {
            var entity = _audioTurnPlateRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == code);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }

        private string[] AudioReason(int LanguageId, string code)
        {
            var entity = _audioReasonRepository.FirstOrDefault(x => x.LanguageId == LanguageId && x.Code == code);
            if (entity == null) return new string[] { "", "" };
            return new string[] { entity.Path + entity.FileName, entity.Content };
        }
        #endregion


        private string GetCode(AirShowDataDto data)
        {
            var code = "";


            switch (data.FlightCirculationStatus)
            {
                case "SCHD":
                    code = (!string.IsNullOrEmpty(data.GateOld) && data.GateOld != data.Gate) ? "BOR_G" : "SCHD";
                    break;
                case "BOR": //BOR_N正常登机广播、BOR_G更改登机口广播
                    code = (!string.IsNullOrEmpty(data.GateOld) && data.GateOld != data.Gate) ? "BOR_G" : "BOR_N";
                    break;
                case "CAN": //J_CAN_T  取消：进港_航班取消通知广播（带时间）                            
                            //J_CAN    取消：进港_航班取消通知广播
                            //C_CAN    取消：出港_航班取消通知广播    

                    //有预计到达时间，且时间为明天
                    if (data.ArrForecastTime.HasValue && data.ArrForecastTime.Value.Date == DateTime.Today.AddDays(1))
                    {
                        code = "CAN_T";
                    }
                    else
                    {
                        code = "CAN";
                    }
                    if (string.IsNullOrEmpty(data.FlightAbnormalReason))
                    {
                        code = code + "_R";
                    }

                    break;
                case "DLY"://J_DLY_T   延误：进港_延误航班预告广播（带时间）
                           //J_DLY     延误：进港_延误航班预告广播
                           //C_DLY_T   延误：出港_航班延误通知广播（带时间）
                           //C_DLY     延误：出港_航班延误通知广播 
                    if (!string.IsNullOrEmpty(data.GateOld) && data.GateOld != data.Gate && data.DeporArrCode == "C") {
                        code = "BOR_G";
                        break;
                    }
                       

                    if (data.DeporArrCode == "C" && data.DepForecastTime.HasValue && data.DepForecastTime.Value.Date == DateTime.Today) //出港
                    {
                        code = "DLY_T";
                    }
                    else if (data.DeporArrCode == "J" && data.ArrForecastTime.HasValue && data.ArrForecastTime.Value.Date == DateTime.Today)//进港
                    {
                        code = "DLY_T";
                    }
                    else
                    {
                        code = "DLY";
                    }

                    if (string.IsNullOrEmpty(data.FlightAbnormalReason))
                    {
                        code = code + "_R";
                    }
                    break;
                case "CKI":
                    code = (!string.IsNullOrEmpty(data.GateOld) && data.GateOld != data.Gate) ? "BOR_G" : "CKI";
                    break;
                default:
                    code = data.FlightCirculationStatus;
                    break;
            }
            return data.DeporArrCode + "_" + code;
        }

        #region Test

        public void Test(string jsonStr)
        {
            hub.RefreshData("j");

        }

        #endregion

    }
}
