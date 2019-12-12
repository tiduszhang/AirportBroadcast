using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Castle.Core.Logging;
using AirportBroadcast.Configuration;
using AirportBroadcast.Domains;
using AirportBroadcast.Domains.Sales;
using AirportBroadcast.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Transactions;

namespace AirportBroadcast.Tasks
{
    public class PushOrderDataWorker : PeriodicBackgroundWorkerBase, ITransientDependency
    {
        private readonly IRepository<Order, long> _orderRepository;
        private readonly ILogger _logger;

        public int EquipmentId { get; set; }

        public bool IsRunNow { get; set; }

        public void SetTimerPeriod(int timer)
        {
            Timer.Period = timer;
        }

        public PushOrderDataWorker(AbpTimer timer,
            IRepository<Order, long> orderRepository,
            ILogger logger) : base(timer)
        {
            timer.Period = 1000 * 60 * 3;
            timer.RunOnStart = true;
            _orderRepository = orderRepository;
            _logger = logger;
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
                string onLineUrl = SettingManager.GetSettingValue(AppSettings.PushToOnline.OnLineUrl).ToString();
                try
                {
                    List<Order> Orders = _orderRepository.GetAll().Where(t =>
                    (t.UploadStatus == PushOnLineStatusEnum.未上传 || t.UploadStatus == PushOnLineStatusEnum.系统异常 || t.UploadStatus == PushOnLineStatusEnum.上传失败 || t.UploadStatus == PushOnLineStatusEnum.找不到线上客运站)
                    && t.CreationTime > befor
                    && t.EquipmentId == EquipmentId
                    && (t.TicketStatus == TicketStatus.出票失败 || t.TicketStatus == TicketStatus.出票成功)
                    ).OrderByDescending(t => t.CreationTime).Take(10).ToList();

                    if (Orders.Count == 0)
                    {
                        Stop();
                        return;
                    }

                    string cityCarrysStr = SettingManager.GetSettingValue(AppSettings.PushToOnline.CityCarryStationId);
                    string carrysStr = SettingManager.GetSettingValue(AppSettings.PushToOnline.CarryStationId);
                    int cityId = int.TryParse(SettingManager.GetSettingValue(AppSettings.PushToOnline.CityId).ToString(), out int result1) ? result1 : 0;
                    string stationId = Orders[0].Equipment.CarryStation.OnlineCarrystaCode;


                    if (!string.IsNullOrEmpty(cityCarrysStr))
                    {
                        var citycarrys = cityCarrysStr.Split(',');
                        foreach (var ccstr in citycarrys)
                        {
                            var cc = ccstr.Split('-');
                            var carrys = cc[1].Split(':');
                            if (carrys[0] == Orders[0].Equipment.CarryStation.OnlineCarrystaCode)
                            {
                                cityId = int.TryParse(cc[0], out int result2) ? result2 : 0;
                                stationId = carrys[1];
                            }
                        }
                    }

                    _logger.InfoFormat("推送订单数据条数为：{0}", Orders.Count);
                    if (UpdataOnLineOrder(Orders, stationId, cityId, onLineUrl))
                    {
                        if (Orders.Count < 10 || IsRunNow)
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
                    _logger.Error("订单数据推送异常！", ex);
                    Stop();
                }
                unitOfWork.Complete();
            }
        }

        [UnitOfWork(false)]
        private bool UpdataOnLineOrder(List<Order> listOrderData, string stationId, int cityId, string onLineUrl)
        {
            //推送订单数据
            int methodCode = 2;//订单
            string pushDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string desFlag = "Y";

            List<object> list = new List<object>();
            foreach (var item in listOrderData)
            {
                string oId = string.Empty;
                if (!string.IsNullOrEmpty(item.AuthorizationTransactionResult))
                {
                    JObject jo = JObject.Parse(item.AuthorizationTransactionResult);
                    if (item.PayMethod == PayMethodEnum.微信)
                    {
                        oId = jo["openid"].ToString();
                    }
                    else if (item.PayMethod == PayMethodEnum.支付宝)
                    {
                        oId = ((JContainer)jo.First).First["buyer_user_id"].ToString();
                    }
                    else if(item.PayMethod == PayMethodEnum.畅付宝微信 || item.PayMethod == PayMethodEnum.畅付宝支付宝)
                    {
                        string datastr = jo["data"].ToString();
                        JObject jo1 = JObject.Parse(datastr);
                        oId = jo1["openId"].ToString();                      
                    }
                }

                dynamic d = new System.Dynamic.ExpandoObject();

                #region 订单信息
                d.id = item.Id;
                d.orderGuid = item.OrderGuid;
                d.payMentStatus = item.PayMentStatus;
                d.ticketStatus = item.TicketStatus;
                d.openId = oId;
                d.ticketAmount = item.TicketAmount;
                d.baoXianAmount = item.BaoXianAmount;
                d.orderTotalPrice = item.OrderTotalPrice;
                d.refundAmount = item.RefundAmount;
                d.orderStatus = item.OrderStatus;

                if (!string.IsNullOrWhiteSpace(item.OrderId))
                {
                    d.orderId = item.OrderId;
                }
                if (item.BaoXianStatus != null)
                {
                    d.baoXianStatus = item.BaoXianStatus;
                }
                if (item.PayMethod != null)
                {
                    d.payMethod = item.PayMethod;
                }
                if (!string.IsNullOrWhiteSpace(item.AuthorizationTransactionId))
                {
                    d.authorizationTransactionId = item.AuthorizationTransactionId;
                }
                if (!string.IsNullOrWhiteSpace(item.AuthorizationTransactionCode))
                {
                    d.authorizationTransactionCode = item.AuthorizationTransactionCode;
                }
                if (!string.IsNullOrWhiteSpace(item.AuthorizationTransactionResult))
                {
                    d.authorizationTransactionResult = item.AuthorizationTransactionResult;
                }
                if (item.PaidDateTime != null)
                {
                    d.paidDateTime = item.PaidDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                d.carryStationId = stationId;
                //if (!string.IsNullOrWhiteSpace(item.CarryStationId))
                //{
                //    d.carryStationId = item.CarryStationId;
                //}
                if (!string.IsNullOrWhiteSpace(item.CarryStationName))
                {
                    d.carryStationName = item.CarryStationName;
                }
                if (!string.IsNullOrWhiteSpace(item.StopId))
                {
                    d.stopId = item.StopId;
                }
                if (!string.IsNullOrWhiteSpace(item.StopName))
                {
                    d.stopName = item.StopName;
                }
                if (!string.IsNullOrWhiteSpace(item.SchId))
                {
                    d.schId = item.SchId;
                }
                d.drvDate = item.DrvDate.ToString("yyyy-MM-dd");
                d.drvTime = Convert.ToDateTime(item.DrvTime.ToString()).ToString("HH:mm");
                d.babyCount = item.BabyCont;
                if (!string.IsNullOrWhiteSpace(item.Remark))
                {
                    d.remark = item.Remark;
                }
                if (!string.IsNullOrWhiteSpace(item.SellTkResult))
                {
                    d.sellTkResult = item.SellTkResult;
                }
                if (!string.IsNullOrWhiteSpace(item.BaoXianResult))
                {
                    d.baoXianResult = item.BaoXianResult;
                }
                if (!string.IsNullOrWhiteSpace(item.MacIp))
                {
                    d.macIp = item.MacIp;
                }
                d.equipmentId = item.EquipmentId;
                d.isDeleted = item.IsDeleted ? 1 : 0;
                if (item.DeleterUserId != null)
                {
                    d.deleterUserId = item.DeleterUserId;
                }
                if (item.DeletionTime != null)
                {
                    d.deletionTime = item.DeletionTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (item.LastModificationTime != null)
                {
                    d.lastModificationTime = item.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (item.LastModifierUserId != null)
                {
                    d.lastModifierUserId = item.LastModifierUserId;
                }
                d.creationTime = item.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"); ;
                if (item.CreatorUserId != null)
                {
                    d.creatorUserId = item.CreatorUserId;
                }
                d.saleList = new List<object>();
                #endregion

                #region 票信息

                foreach (var t in item.OrderItems)
                {
                    List<object> baoXianlist = new List<object>();
                    if (t.BaoXianStatus != null && t.BaoXianStatus != BaoXianStatus.未投保)
                    {
                        dynamic b = new System.Dynamic.ExpandoObject();
                        b.baoXianPrice = t.BaoXianPrice;
                      
                        b.cardType = 1;

                        if (!string.IsNullOrEmpty( item.BaoXianCompanyName))
                        {
                            b.baoXianCompanyName = item.BaoXianCompanyName;
                        }
                        if (t.BaoXianCompanyProductId.HasValue)
                        {
                            b.baoXianCompanyProductId = t.BaoXianCompanyProductId;

                            b.baoXianCompensation = t.BaoXianCompanyProduct.Compensation;
                            b.baoXianAccident = t.BaoXianCompanyProduct.Accident;
                            b.baoXianMedicalCare = t.BaoXianCompanyProduct.MedicalCare;

                        }
                        if (!string.IsNullOrEmpty(item.BaoXianProductName))
                        {
                            b.baoXianProductName = item.BaoXianProductName;
                        }


                       


                        if (!string.IsNullOrWhiteSpace(t.InputNo))
                        {
                            b.inputNo = t.InputNo;
                        }
                        if (!string.IsNullOrWhiteSpace(t.OutPutNo))
                        {
                            b.outPutNo = t.OutPutNo;
                        }
                        if (t.BaoXianStatus != null && t.BaoXianStatus != BaoXianStatus.未投保)
                        {
                            b.baoXianStatus = t.BaoXianStatus;
                        }
                        if (t.BaoXianStatus != null && t.BaoXianStatus != BaoXianStatus.未投保)
                        {
                            baoXianlist.Add(b);
                        }
                    }

                    dynamic s = new System.Dynamic.ExpandoObject();
                    s.id = t.Id;
                    if (!string.IsNullOrWhiteSpace(t.TicketNum))
                    {
                        s.ticketNum = t.TicketNum;
                    }
                    if (!string.IsNullOrWhiteSpace(t.Name))
                    {
                        s.name = t.Name;
                    }
                    if (!string.IsNullOrWhiteSpace(t.Phone))
                    {
                        s.phone = t.Phone;
                    }
                    if (!string.IsNullOrWhiteSpace(t.Sn))
                    {
                        s.sn = t.Sn;
                    }
                    if (!string.IsNullOrWhiteSpace(t.SeatNo))
                    {
                        s.seatNo = t.SeatNo;
                    }
                    s.ticketCatalog = t.TicketCatalog;
                    s.ticketPrice = t.TicketPrice;
                    if (t.HasBaby != null)
                    {
                        s.hasBaby = t.HasBaby.GetValueOrDefault() ? 1 : 0;
                    }
                    if (t.Name.ToLower().Contains("xx") || t.Name == "王稳" || t.TicketCatalog == TicketCatalogEnum.半价票)
                    {
                        s.realCard = 0;
                    }
                    else
                    {
                        s.realCard = 1;
                    }

                    if (t.BaoXianStatus != null && t.BaoXianStatus != BaoXianStatus.未投保)
                    {
                        s.baoXianList = baoXianlist;
                    }

                    d.saleList.Add(s);
                }

                #endregion

                list.Add(d);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string data = JsonConvert.SerializeObject(list, settings);
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
            _logger.InfoFormat("推送订单数据为：{0}", JsonConvert.SerializeObject(request));
            string strRespone = HttpClientHelper.HttpPostPushData(JsonConvert.SerializeObject(request), "", onLineUrl);
            _logger.InfoFormat("推送订单返回数据为：{0}", strRespone);

            var respone = JsonConvert.DeserializeObject<OnLineResult>(strRespone);
            bool resultflag = false;
            if (respone.flag == "1")
            {
                foreach (var item in listOrderData)
                {
                    item.UploadStatus = PushOnLineStatusEnum.上传成功;
                    item.UploadErorrMsg = "";
                }
                _logger.Info("推送订单数据成功！");
                resultflag = true;
            }
            else if (respone.flag == "0")
            {
                foreach (var item in listOrderData)
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
                resultflag = false;
            }

            return resultflag;
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
}
