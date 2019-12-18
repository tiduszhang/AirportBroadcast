using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航显设备对接帮助类
    /// </summary>
    public class TAClientHelp
    {
        /// <summary>
        /// 连接
        /// </summary>
        private static void Connection()
        {
            if (TAClientConnector.TAGetLinkStatus() == TAClientConnector.TA_LINKOK)
            {
                return;
            }

            var link = TAClientConnector.TAStart();
            if (link == TAClientConnector.TA_SUCCESS)
            {
                TAClientConnector.TAPutMsgExt("I'm coming!.........");
            }
            else
            {
                //连接失败
            }
            System.Threading.Thread.Sleep(100);
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        private static bool isRun = false;

        /// <summary>
        /// 消息队列
        /// </summary>
        private static List<string> _Messages = new List<string>();

        /// <summary>
        /// 开始获取数据
        /// </summary>
        public static void StartRecivce()
        {
            if (isRun)
            {
                return;
            }

            Task.Factory.StartNew(() =>
            {
                isRun = true;

                TAClientHelp.Connection();

                StringBuilder msg = new StringBuilder(4096);
                while (isRun)
                {
                    //System.Threading.Thread.SpinWait(1);
                    System.Threading.Thread.Sleep(1);
                    try
                    {
                        if (TAClientConnector.TAGetLinkStatus() != TAClientConnector.TA_LINKOK)
                        {
                            TAClientConnector.TAClose();
                            System.Threading.Thread.Sleep(100);
                            TAClientHelp.Connection();
                            continue;
                        }

                        var length = 0;
                        while ((length = TAClientConnector.TAGetMsgExt(msg)) >= 0 && isRun)
                        {
                            System.Threading.Thread.Sleep(1);
                            //System.Threading.Thread.SpinWait(1);
                            lock (_Messages)
                            {
                                _Messages.Add(msg.ToString().Trim());
                                msg.Clear();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public static void Send(string message)
        {
            TAClientConnector.TAPutMsgExt(message);
        }

        /// <summary>
        /// 停止获取数据
        /// </summary>
        public static void StopRecivce()
        {
            isRun = false;
            TAClientConnector.TAClose();
        }

        /// <summary>
        /// 获取缓存内的所有消息内容
        /// </summary>
        /// <returns></returns>
        public static string[] GetMessages()
        {
            if (_Messages == null)
            {
                return null;
            }
            string[] message = null;
            lock (_Messages)
            {
                if (_Messages.Count > 0)
                {
                    message = _Messages.ToArray();
                    _Messages.Clear();
                }
            }
            return message;
        }
    }
}
