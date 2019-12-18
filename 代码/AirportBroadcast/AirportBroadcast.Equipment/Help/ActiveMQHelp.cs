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
                        System.Threading.Thread.Sleep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
