using Abp.Dependency;
using Abp.Domain.Repositories;
using AirportBroadcast.AudioControl;
using AirportBroadcast.ActiveMQ.Dto;
using AirportBroadcast.Domain.activeMq;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Castle.Core.Logging;
using AirportBroadcast.Chat;
using Abp.Domain.Uow;
using System.Net;
using System.Net.Sockets;
using Apache.NMS.Util;
using log4net.Repository.Hierarchy;
using AirportBroadcast.Chat.Dto;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain;
using AirportBroadcast.Utility;
using System.IO;

namespace AirportBroadcast.ActiveMQ
{
    public class ActiveMQListener : ISingletonDependency
    {
        public string UUID = "";
        private readonly IRepository<ReceiveJson, long> detailRepository;
        private readonly IRepository<AirshowData, long> airshowDataRepository;
        private readonly ICommAudioTempleAppService _play;
        private readonly IPlayAudioHub hub;
        private readonly ILogger logger;
        private readonly IWavCombine _wavCombine;
        private readonly IRepository<CommAudioFileName, long> _commAudioFileNameRepository;
        private readonly IRepository<PlayAudioLog, long> _logRepository;
        private readonly ISqlExecuter sqlExecuter;

        public IActiveUnitOfWork CurrentUnitOfWork { get; set; }

        public delegate void RevMessageHandler<ITextMessage>(ITextMessage requestInfo);
        public event RevMessageHandler<ITextMessage> ReceivedData;

        public ActiveMQListener(IRepository<ReceiveJson, long> detailRepository,
            IRepository<AirshowData, long> airshowDataRepository,
            IPlayAudioHub hub,
            IWavCombine _wavCombine,
             ISqlExecuter sqlExecuter,
            IRepository<PlayAudioLog, long> _logRepository,
            IRepository<CommAudioFileName, long> _commAudioFileNameRepository,
            ICommAudioTempleAppService _play,
            ILogger logger
            )
        {
            this.detailRepository = detailRepository;
            this.airshowDataRepository = airshowDataRepository;
            this._commAudioFileNameRepository = _commAudioFileNameRepository;
            this.logger = logger;
            this._play = _play;
            this.hub = hub;
            UUID = Guid.NewGuid().ToString();
            this._logRepository = _logRepository;
            this._wavCombine = _wavCombine;
            this._wavCombine.PlayStateCallback += _wavCombine_PlayStateCallback;
            this.sqlExecuter = sqlExecuter;
        }

        /// <summary>
        /// 队列IP：以机场所分配的服务器IP为准
        /// </summary>
        public static string ActiveMQUrl => ConfigurationManager.AppSettings["ActiveMQUrl"];
        public static string QueueName => ConfigurationManager.AppSettings["QueueName"];
        public static string UserName => ConfigurationManager.AppSettings["UserName"];
        public static string Pwd => ConfigurationManager.AppSettings["Pwd"];
        public static string Selector => ConfigurationManager.AppSettings["Selector"];

        public static string ActiveMQUrlBak => ConfigurationManager.AppSettings["ActiveMQUrlBak"];
        public static string QueueNameBak => ConfigurationManager.AppSettings["QueueNameBak"];
        public static string UserNameBak => ConfigurationManager.AppSettings["UserNameBak"];
        public static string PwdBak => ConfigurationManager.AppSettings["PwdBak"];
        public static string SelectorBak => ConfigurationManager.AppSettings["SelectorBak"];
        IConnection connection;
        /// <summary>
        /// 初始化ActiveMQ
        /// </summary>
        /// <param name="flag">0表示主链接，1 表示航显链接，-1表示关闭ActiveMQ</param>
        public void Initialize(int flag)
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                }

                string url = ActiveMQUrl, userName = UserName, pwd = Pwd, queueName = QueueName;
                switch (flag)
                {
                    case 0: url = ActiveMQUrl; userName = UserName; pwd = Pwd; queueName = QueueName; break;
                    case 1: url = ActiveMQUrlBak; userName = UserNameBak; pwd = PwdBak; queueName = QueueName; break;
                    default: return;
                }

                //创建连接工厂
                // IConnectionFactory factory = new ConnectionFactory( new Uri(string.Format("activemq:failover:(tcp://{0})", url)));
                IConnectionFactory factory = new ConnectionFactory(new Uri(string.Format("failover:(tcp://{0})?randomize=false", url)));

                //通过工厂创建连接 
                connection = factory.CreateConnection(userName, pwd);

                //connection = factory.CreateConnection();
                //连接服务器端的标识
                connection.ClientId = GetLocalIP() + "_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// "firstQueueListener";                     
                                                                                                        //启动连接
                connection.Start();
                connection.ConnectionInterruptedListener += Connection_ConnectionInterruptedListener;
                connection.ConnectionResumedListener += Connection_ConnectionResumedListener;
                connection.ExceptionListener += Connection_ExceptionListener;

                //通过连接创建对话
                ISession session = connection.CreateSession();



                // IMessageConsumer consumer = session.CreateConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQQueue(queueName), Selector);
                IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(queueName), connection.ClientId, null, false);



                //注册监听事件
                consumer.Listener += new MessageListener(consumer_Listener);


                ReceivedData += ActiveMQListener_ReceivedData;
                // consumer.ReceiveNoWait();
                VoiceBroadcastUtil.jTTS_Init(null, null);
            }
            catch (Exception e)
            {

            }
        }

        private void Connection_ExceptionListener(Exception ex)
        {
            logger.Error("MQ链接错误：" + ex.Message, ex);
        }

        private void Connection_ConnectionResumedListener()
        {
            //已恢复容错连接
            logger.Error("MQ链接：已恢复容错连接");
        }

        private void Connection_ConnectionInterruptedListener()
        {
            //容错连接中断
            logger.Error("MQ链接：容错连接中断");
        }

        /// <summary>
        /// 初始化ActiveMQ
        /// </summary>
        /// <param name="flag">0表示主链接，1 表示航显链接，-1表示关闭ActiveMQ</param>
        public void Initialize_1(int flag)
        {

            if (connection != null)
            {
                connection.Close();
            }
            string url = ActiveMQUrl, userName = UserName, pwd = Pwd, queueName = QueueName;
            switch (flag)
            {
                case 0: url = ActiveMQUrl; userName = UserName; pwd = Pwd; queueName = QueueName; break;
                case 1: url = ActiveMQUrlBak; userName = UserNameBak; pwd = PwdBak; queueName = QueueName; break;
                default: return;
            }
            //创建连接工厂
            IConnectionFactory factory = new NMSConnectionFactory(new Uri(string.Format("activemq:failover:(tcp://{0})", url)));
            //通过工厂创建连接 
            connection = factory.CreateConnection(userName, pwd);

            //connection = factory.CreateConnection();
            var ip = GetLocalIP();
            //连接服务器端的标识
            //connection.ClientId = ip + "_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// "firstQueueListener";                                                                                     //启动连接
            ISession session = connection.CreateSession();
            //发布/订阅模式，适合一对多的情况

            //IDestination destination  = SessionUtil.GetDestination(session, "topic://" + ip);
            connection.Start();
            //通过连接创建对话    
            IMessageConsumer consumer = session.CreateDurableConsumer(session.GetTopic(QueueName), connection.ClientId, null, false);
            //注册监听事件
            consumer.Listener += new MessageListener(consumer_Listener);
            ReceivedData += ActiveMQListener_ReceivedData;
            // consumer.ReceiveNoWait();
        }

        private void ActiveMQListener_ReceivedData(ITextMessage requestInfo)
        {
            var json = requestInfo.Text;

            DoMsg(json);
        }

        private void consumer_Listener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;

            if (ReceivedData != null)
            {
                ReceivedData.Invoke(msg);
            }
        }


        
        public void DoMsg(string json)
        {
            //获取到ActiveMQ数据了           

            //todo: MQ内获取到的航班信息内容解析
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    var oldgate = "";
                    var rj = detailRepository.Insert(new ReceiveJson { ReciveTime = DateTime.Now, Content = json });
                    var jsonAirshowData = Newtonsoft.Json.JsonConvert.DeserializeObject<AirShowDataDto>(json);
                    var AirshowData = Mapper.Map<AirshowData>(jsonAirshowData);// jsonAirshowData.MapTo<AirshowData>();

                    if (string.Compare(AirshowData.RouteType.Trim(), "DELETEALL") == 0)
                    {
                        _commAudioFileNameRepository.Delete(x => x.Id > 0);
                        airshowDataRepository.Delete(x => x.Id > 0); 
                        saveRjson(rj, "RouteType 值为 DELETEALL，删除全部数据，不播放!");
                        return;
                    }
                    if (string.Compare(AirshowData.FestNo3.Trim(), "LUM") == 0 && string.Compare(AirshowData.ForgNo3.Trim(), "LUM") == 0)
                    {
                        saveRjson(rj, "进出港都在本区域,不播放!");
                        return;
                    }
                    if (string.IsNullOrEmpty(AirshowData.DeporArrCode))
                    {
                       
                        if (string.Compare(AirshowData.FestNo3.Trim(), "LUM") == 0)
                        {
                            //落场  芒市 == J
                            AirshowData.DeporArrCode = "J";
                        }
                        else if (string.Compare(AirshowData.ForgNo3.Trim(), "LUM") == 0)
                        {
                            //起场  芒市 ==C
                            AirshowData.DeporArrCode = "C";
                        }
                        else {
                            saveRjson(rj, "进出港标记为空并且起场落场都不在本区域,不播放!");
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(AirshowData.FlightNo2) || string.IsNullOrEmpty(AirshowData.FlightCirculationStatus))
                    {
                        saveRjson(rj, "航班号或进出港标或者扭转状态记为空,不播放!");
                        return;
                    }
                    if (string.Compare(AirshowData.FlightCirculationStatus.Trim(), "SCHD") == 0)
                    {
                        saveRjson(rj, "航班状态为SCHD,不播放!");
                       // return;
                    }

                    //时间有效性过滤
                    if (string.Compare(AirshowData.FlightCirculationStatus.Trim(), "ARR") == 0 || string.Compare(AirshowData.FlightCirculationStatus.Trim(), "NST") == 0)
                    {
                        TimeSpan ts = DateTime.Now - AirshowData.ArriveTime;
                        if (ts.TotalMinutes > 10)
                        {
                            saveRjson(rj, string.Format("【进港】过期数据：{0} , 不播放！", AirshowData.ArriveTime.ToString().Trim()));
                            return;
                        }
                    }
                    DateTime today = DateTime.Today;
                    var entity = airshowDataRepository.FirstOrDefault(x => x.FlightNo2 == AirshowData.FlightNo2 && x.ReciveTime> today);
                    if (entity != null)
                    {
                        if(AirshowData.RouteType== "DELETE")
                        {
                            airshowDataRepository.Delete(entity);
                            saveRjson(rj, "RouteType 值为 DELETE，不播放");
                            return;
                        }

                        if (entity.FlightNo2 == AirshowData.FlightNo2 &&
                            entity.FlightCirculationStatus == AirshowData.FlightCirculationStatus &&
                            entity.DeporArrCode == AirshowData.DeporArrCode &&
                            entity.DepForecastTime == AirshowData.DepForecastTime &&
                            entity.ArrForecastTime == AirshowData.ArrForecastTime &&
                            entity.Gate == AirshowData.Gate)
                        {
                            saveRjson(rj, string.Format("已有数据Id：{0} ,同一条数据，不播放！",entity.Rid.ToString().Trim()));
                            return;
                        }

                        if (entity.DeporArrCode == "J")
                        {
                           
                            if (GetSortByStatus(entity.FlightCirculationStatus, entity.DeporArrCode) < GetSortByStatus(AirshowData.FlightCirculationStatus, entity.DeporArrCode)) {
                                saveRjson(rj, string.Format("【进港】已有数据Id：{0} , 流转状态：{1}；新到数据流转状态：{2} 。播放顺序原因，不播放！", entity.Rid.ToString().Trim(),entity.FlightCirculationStatus, AirshowData.FlightCirculationStatus));
                                return;
                            } 
                        }
                        else if (entity.DeporArrCode == "C")
                        {
                            if (GetSortByStatus(entity.FlightCirculationStatus, entity.DeporArrCode) < GetSortByStatus(AirshowData.FlightCirculationStatus, entity.DeporArrCode))
                            {
                                saveRjson(rj, string.Format("【出港】已有数据Id：{0} , 流转状态：{1}；新到数据流转状态：{2} 。播放顺序原因，不播放！", entity.Rid.ToString().Trim(), entity.FlightCirculationStatus, AirshowData.FlightCirculationStatus));
                                return;
                            }
                        }
                        else
                        {
                            saveRjson(rj, string.Format("错误的进出港标记：{0}！", entity.DeporArrCode));
                            return;
                        }
                        if (!string.IsNullOrEmpty(AirshowData.Gate) && entity.Gate != AirshowData.Gate) {
                            TimeSpan timeSpan = AirshowData.DepartTime - DateTime.Now ;
                            if (timeSpan.TotalMinutes >= 0 && timeSpan.TotalMinutes <= 120 && Convert.ToInt32(AirshowData.Gate.Trim())>0)
                            {
                                if (entity.FlightCirculationStatus == "BOR"
                                && AirshowData.FlightCirculationStatus == "BOR")
                                {
                                    oldgate = entity.Gate;
                                }
                                else if (entity.FlightCirculationStatus == "SCHD"
                                    && AirshowData.FlightCirculationStatus == "SCHD")
                                {
                                    oldgate = entity.Gate;
                                }
                                else if (entity.FlightCirculationStatus == "CKI"
                                 && AirshowData.FlightCirculationStatus == "CKI")
                                {
                                    oldgate = entity.Gate;
                                }
                                else if (entity.FlightCirculationStatus == "DLY"
                                 && AirshowData.FlightCirculationStatus == "DLY")
                                {
                                    oldgate = entity.Gate;
                                }
                            }
                        }                       
                        AirshowData.Id = entity.Id;
                    }

                    AirshowData.Rid = rj.Id;
                    AirshowData = airshowDataRepository.InsertOrUpdate(AirshowData);
                    // CurrentUnitOfWork.SaveChanges(); 暂时注释
                    try
                    {
                        hub.RefreshData(AirshowData.DeporArrCode);

                    }
                    catch (Exception ex)
                    {
                        logger.Error("未登录:" + ex.Message, ex);
                    }
                  
                    var dto = Mapper.Map<AirShowDataDto>(AirshowData);
                    if (dto.Gate != oldgate && !string.IsNullOrEmpty(oldgate))
                    {
                        dto.GateOld = oldgate;
                    }
                    _play.PlayAudioTemple(dto);
                }
                catch (Exception ex)
                {
                    logger.Error("ActiveMQ过来数据异常:" + ex.Message, ex);
                }
            }
        }

        private void saveRjson(ReceiveJson rj,string remark)
        {
            rj.Remark += remark;
            detailRepository.Update(rj);
        }

        private void _wavCombine_PlayStateCallback(Domain.wavFileInfo.PlayStateData input)
        {
            try
            {
                var entity = _commAudioFileNameRepository.FirstOrDefault(x => x.FileName == input.FileName);
                if (entity == null)
                {
                   var name = Path.GetFileName(input.FileName);
                    if (!string.IsNullOrEmpty(name) && name.StartsWith("HandTxt")) {
                       if(input.PlayState == 1) _play.SetPlayWayAuto();
                        logger.InfoFormat("手动插播：{0},状态：{1}", input.FileName, input.PlayState);
                    }
                    else
                    {
                        logger.ErrorFormat("数据库中未找到正在播放的文件：{0},状态：{1}(0开始播放,1播放结束)", input.FileName, input.PlayState);
                    }

                   
                  
                    return;
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
                _commAudioFileNameRepository.Update(entity);
                _logRepository.Insert(new PlayAudioLog()
                {
                    FileName = input.FileName,
                    Remark = entity.PlayStatus.ToString() + ":" + entity.Remark
                });
                var ports = _commAudioFileNameRepository.GetAllList(x => x.PlayStatus == PlayStatus.开始播放).Select(x => x.PlayPort).ToList();
                var port = new TpPortDto();
                ports.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x) && x.Length == 8)
                    {
                        var cs = x.ToArray();
                        port.P1 = (cs[0] == '1' || port.P1);
                        port.P2 = (cs[1] == '1' || port.P2);
                        port.P3 = (cs[2] == '1' || port.P3);
                        port.P4 = (cs[3] == '1' || port.P4);
                        port.P5 = (cs[4] == '1' || port.P5);
                        port.P6 = (cs[5] == '1' || port.P6);
                        port.P7 = (cs[6] == '1' || port.P7);
                        port.P8 = (cs[7] == '1' || port.P8);
                    }
                });
                try
                {
        
                    hub.RefreshNowPlayAudio(port);
                }catch(Exception ex)
                {
                    logger.Error("未登录:" + ex.Message, ex);
                 //   logger.ErrorFormat("播放状态数据改变通知前台异常:{0}", ex.Message + ex.Source);
                }
               
            }
            catch (Exception ex)
            {
                var s = ex;
                logger.ErrorFormat("播放状态数据改变异常{0}:", ex.Message + ex.Source);
            }

        }


        public string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        private int GetSortByStatus_bak(string status, string deporarr)
        {
            if (deporarr == "J")
            {
                switch (status)
                { 
                    case "FBAG": return 0;//行礼提取
                    case "ARR": return 1; //到达
                    case "NST": return 1; //到达
                    case "DLY": return 2; //延误
                    case "CAN": return 2; //取消
                    case "RTN": return 2; //返航
                    default: return 99; 
                }
            }
            else
            {
                switch (status)
                { 
                    case "DEP": return 0;   //起飞           
                    case "ONR": return 0;  //起飞     
                    case "READY": return 1;//客齐
                    case "POK": return 1;//客齐
                         
                    case "LBD": return 2; //催促登机
                    case "BOR": return 2; //登机

                    case "CAN": return 3; //取消
                    case "DLY": return 3; //延误
                    case "CKO": return 3; //结束值机
                    case "CKI": return 3; //开始值机
                    default: return 99;

                }
            }

        }

        private int GetSortByStatus(string status, string deporarr)
        {
            if (deporarr == "J")
            {
                switch (status)
                {

                    case "FBAG": return 0;
                    case "ARR": return 1;
                    case "NST": return 2;
                    //case "DLY": return 3;
                    //case "CAN": return 3;
                    default: return 99;

                }
            }
            else
            {
                switch (status)
                {
                    //case "CAN": return 0;
                    //case "DLY": return 0;

                    case "DEP": return 90;
                    case "POK":
                    case "READY": return 93;
                    case "LBD":
                    case "URBOR": return 94;
                    case "BOR": return 95;
                    case "CKOFF":
                    case "CKO": return 97;
                    case "CKI": return 98;
                    default: return 99;

                }
            }

        }
    }
}
