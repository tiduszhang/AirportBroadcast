using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// ActiveMQ帮助类
    /// </summary>
    public class ActiveMQHelp
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public static void SendMessage(string message)
        {
            var configFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "tac.ini");
            if (!System.IO.File.Exists(configFile))
            {
                return;
            }
            var activeMQUrl = IniFileHelp.Read("MQSERVER", "ActiveMQUrl", "", configFile);
            var aueueName = IniFileHelp.Read("MQSERVER", "QueueName", "", configFile);
            var password = IniFileHelp.Read("MQSERVER", "Password", "", configFile);
            var userName = IniFileHelp.Read("MQSERVER", "UserName", "", configFile);

            try
            {
                IConnectionFactory factory = new ConnectionFactory(String.Format("tcp://{0}/", activeMQUrl));
                using (IConnection connection = factory.CreateConnection(userName, password))
                {
                    //Create the Session   
                    using (ISession session = connection.CreateSession())
                    {
                        //Create the Producer for the topic/queue   
                        IMessageProducer prod = session.CreateProducer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(aueueName));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = message;
                        prod.Send(msg);
                        LogHelp.WriteToLog(String.Format("向MQ服务{0}发送消息：【{1}】", activeMQUrl, message), log4net.Core.Level.Info);
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelp.WriteToLog(String.Format("向MQ服务{0}发送消息：【{1}】，出现异常：【{2}】", activeMQUrl, message, ex.ToString()), log4net.Core.Level.Error);
            }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        static bool isRun = false;

        /// <summary>
        /// 启动发送
        /// </summary>
        public static void StartSend()
        {
            if (isRun)
            {
                return;
            }
            isRun = true;


            Task.Factory.StartNew(() =>
            {
                while (isRun)
                {
                    System.Threading.Thread.Sleep(1);
                    var commands = CommandHelp.GetCommands();
                    if (commands == null)
                    {
                        continue;
                    }

                    commands.ToList().ForEach(command =>
                    {
                        try
                        {
                            var commandString = command.CommandData.CreateMQCommand();
                            if (!String.IsNullOrWhiteSpace(commandString))
                            {
                                SendMessage(commandString);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelp.WriteToLog(String.Format("航显系统数据转换到系统统一格式时出现异常：【{0}】", ex.ToString()), log4net.Core.Level.Error); 
                        }
                    });
                }

            });

        }

        /// <summary>
        /// 停止发送
        /// </summary>
        public static void StopSend()
        {
            isRun = false;
        }
    }
}
