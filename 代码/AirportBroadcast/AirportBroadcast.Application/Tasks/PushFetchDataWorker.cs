using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Castle.Core.Logging;
using AirportBroadcast.Configuration;
using AirportBroadcast.Domains.Featch;
using AirportBroadcast.Domains.Sales;
using AirportBroadcast.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace AirportBroadcast.Tasks
{
    
    public class PushFetchDataWorker : PeriodicBackgroundWorkerBase, ITransientDependency
    {
        private readonly IRepository<WriteFetchData,long> _writeFetchDataRepository;

        private readonly ILogger _logger;

        public int EquipmentId { get; set; }

        public bool IsRunNow { get; set; }

        public void SetTimerPeriod(int timer)
        {
            Timer.Period = timer;
        }

        public PushFetchDataWorker(
            AbpTimer timer,
            IRepository<WriteFetchData,long> writeFetchDataRepository,
            ILogger logger) : base(timer)
        {
            _writeFetchDataRepository = writeFetchDataRepository;
            _logger = logger;
            timer.Period = 1000 * 60 * 3;
            timer.RunOnStart = true;
            IsRunNow = false;
        }

        [UnitOfWork(false)]
        protected override void DoWork()
        {
            using (var unitOfWork = UnitOfWorkManager.Begin(new UnitOfWorkOptions() { IsTransactional = false, Scope = TransactionScopeOption.Suppress, }))
            {
                var days = int.TryParse(SettingManager.GetSettingValue(AppSettings.PushToOnline.StartDays).ToString(), out int result) ? result : 0;
                DateTime monthsAgo = DateTime.Today.AddDays(-days);
                DateTime startDays = Convert.ToDateTime(SettingManager.GetSettingValue(AppSettings.PushToOnline.StartDate).ToString());
                DateTime befor = DateTime.Compare(monthsAgo, startDays) < 0 ? startDays : monthsAgo;



                try
                {
                    List<WriteFetchData> fetchTickets = _writeFetchDataRepository.GetAll().Where(t => (t.UploadStatus == PushOnLineStatusEnum.未上传 || t.UploadStatus == PushOnLineStatusEnum.系统异常 || t.UploadStatus == PushOnLineStatusEnum.上传失败 || t.UploadStatus == PushOnLineStatusEnum.找不到线上客运站)
                    && t.CreationTime > befor
                    && t.EquipmentId == EquipmentId
                    ).OrderByDescending(t => t.CreationTime).Take(10).ToList();

                    if (fetchTickets.Count == 0)
                    {
                        Stop();
                        return;
                    }

                    string cityCarrysStr = SettingManager.GetSettingValue(AppSettings.PushToOnline.CityCarryStationId);
                    var carrysStr = SettingManager.GetSettingValue(AppSettings.PushToOnline.CarryStationId);
                    int cityId = int.TryParse(SettingManager.GetSettingValue(AppSettings.PushToOnline.CityId).ToString(), out int result1) ? result1 : 0;
                    var stationId = fetchTickets[0].Equipment.CarryStation.Code;


                    //_logger.InfoFormat("配置的乘车站：{0}", cityCarrysStr);

                    if (!string.IsNullOrEmpty(cityCarrysStr))
                    {
                        var citycarrys = cityCarrysStr.Split(',');
                        foreach (var ccstr in citycarrys)
                        {
                            var cc = ccstr.Split('-');
                            var carrys = cc[1].Split(':');
                            if (carrys[0] == fetchTickets[0].Equipment.CarryStation.Code)
                            {
                                cityId = int.TryParse(cc[0], out int result2) ? result2 : 0;
                                stationId = carrys[1];
                            }
                        }
                    }


                    string onLineUrl = SettingManager.GetSettingValue(AppSettings.PushToOnline.OnLineUrl).ToString();

                    _logger.InfoFormat("推送取票数据条数为：{0}", fetchTickets.Count);

                    if (UpdataOnLineFetch(fetchTickets, cityId, stationId, onLineUrl))
                    {
                        if (fetchTickets.Count < 10 || IsRunNow)
                        {
                            Stop();
                        }
                        if (IsRunNow)
                        {
                            IsRunNow = false;
                            Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("取票数据推送异常！", ex);
                    Stop();
                }
                unitOfWork.Complete();
            }
        }

        [UnitOfWork(false)]
        private bool UpdataOnLineFetch(List<WriteFetchData> listWriteFetchData, int cityId, string stationId, string onLineUrl)
        {
            //推送取票数据 
            int methodCode = 1;//取票
            string pushDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string desFlag = "Y";


            List<object> list = new List<object>();
            foreach (var item in listWriteFetchData)
            {
                dynamic d = new System.Dynamic.ExpandoObject();
                d.id = item.Id;
                d.ticketCount = item.TicketCount;
                d.creationTime = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                d.creationDate = item.CreationTime.ToString("yyyy-MM-dd");
                d.equipmentId = item.EquipmentId;
                d.cardType = 1;
                if (!string.IsNullOrWhiteSpace(item.Openid))
                {
                    d.openId = item.Openid;
                }

                if (!string.IsNullOrWhiteSpace(item.RealName) && !string.IsNullOrWhiteSpace(item.RealSn))
                {
                    d.realCard = 1;
                }
                else
                {
                    d.realCard = 0;
                }

                if (!string.IsNullOrWhiteSpace(item.Query))
                {
                    d.query = item.Query;
                }
                if (!string.IsNullOrWhiteSpace(item.EndStation))
                {
                    d.endStation = item.EndStation;
                }
                if (!string.IsNullOrWhiteSpace(item.StartStation))
                {
                    d.startStation = item.StartStation;
                }
                if (!string.IsNullOrWhiteSpace(item.TicketNo))
                {
                    d.ticketNo = item.TicketNo;
                }
                if (!string.IsNullOrWhiteSpace(item.DrvDate))
                {
                    d.drvDate = Convert.ToDateTime(item.DrvDate).ToString("yyyy-MM-dd HH:mm");
                }
                if (!string.IsNullOrWhiteSpace(item.Sn))
                {
                    d.sn = item.Sn;
                }
                if (!string.IsNullOrWhiteSpace(item.PassengerName))
                {
                    d.passengerName = item.PassengerName;
                }
                if (!string.IsNullOrWhiteSpace(item.SchId))
                {
                    d.schId = item.SchId;
                }
                if (!string.IsNullOrWhiteSpace(item.Seat))
                {
                    d.seat = item.Seat;
                }
                if (!string.IsNullOrWhiteSpace(item.Result))
                {
                    d.result = item.Result;
                }
                if (!string.IsNullOrWhiteSpace(item.GetTkWay))
                {
                    d.takeMethod = item.GetTkWay;
                }
                
                list.Add(d);
            }
            string data = JsonConvert.SerializeObject(list);
            string desData = Encrypt.DesEncryptPush(data, SettingManager.GetSettingValue(AppSettings.PushToOnline.PushMsgDesKey));

            string privateKey = SettingManager.GetSettingValue(AppSettings.PushToOnline.PrivateKey).ToString();
            string strSign = string.Format("cityId={0}&data={1}&dataDesFlag={2}&methodCode={3}&pushDate={4}&stationId={5}{6}", cityId, desData, desFlag, methodCode, pushDate, stationId, privateKey);
            string sign = MD5Encrypt(strSign).ToUpper();

            OnLineRequest request = new OnLineRequest
            {
                methodCode = methodCode,
                pushDate = pushDate,
                cityId = cityId,
                stationId = stationId,
                dataDesFlag = desFlag,
                data = desData,
                sign = sign
            };
            _logger.InfoFormat("推送取票数据为：{0}", JsonConvert.SerializeObject(request));
            string strRespone = HttpClientHelper.HttpPostPushData(JsonConvert.SerializeObject(request), "", onLineUrl);
            _logger.InfoFormat("推送取票返回数据为：{0}", strRespone);

            var respone = JsonConvert.DeserializeObject<OnLineResult>(strRespone);

            if (respone.flag == "1")
            {
                foreach (var item in listWriteFetchData)
                {
                    item.UploadStatus = PushOnLineStatusEnum.上传成功;
                    item.UploadErorrMsg = "";
                }
                _logger.Info("推送取票数据成功！");
                return true;
            }
            else if (respone.flag == "0")
            {
                foreach (var item in listWriteFetchData)
                {
                    item.UploadStatus = PushOnLineStatusEnum.上传失败;
                    item.UploadErorrMsg = respone.failDesc;

                    if (respone.failCode == "000039")
                    {
                        item.UploadStatus = PushOnLineStatusEnum.系统异常;
                    }
                    else if (respone.failCode == "000529")
                    {
                        item.UploadStatus = PushOnLineStatusEnum.参数不合法;
                    }
                    else if (respone.failCode == "000574")
                    {
                        item.UploadStatus = PushOnLineStatusEnum.签名失败;
                    }
                    else if (respone.failCode == "001074")
                    {
                        item.UploadStatus = PushOnLineStatusEnum.找不到线上客运站;
                    }
                }
                _logger.Error(string.Format("推送数据失败！错误码：{0}。错误信息：{1}", respone.failCode, respone.failDesc));
                return false;
            }
            return false;
        }


        /// <summary>
        /// 用MD5加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <returns></returns>
        public string MD5Encrypt(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
    }

    public class OnLineRequest
    {
        //方法编码：1：取票信息，2：订单信息
        public int methodCode { get; set; }

        //推送时间（yyyy-MM-dd HH:mm:ss）
        public string pushDate { get; set; }

        //畅途网城市id（线上）
        public int cityId { get; set; }

        //线下接口客运站id（线下，并非畅途网客运站id，也并非客运研发接口id）
        public string stationId { get; set; }

        //data字符串是否加密Y加密,N不加密（默认N）
        public string dataDesFlag { get; set; }

        //业务数据（JSON串）
        public string data { get; set; }

        //签名
        public string sign { get; set; }
    }

    public class OnLineResult
    {
        //结果：1成功，0失败
        public string flag { get; set; }

        //描述（具体描述）
        public string failDesc { get; set; }

        //失败编码  000039:系统异常   000529:参数不合法  000574:签名失败  001074:找不到线上客运站
        public string failCode { get; set; }
    }
}
