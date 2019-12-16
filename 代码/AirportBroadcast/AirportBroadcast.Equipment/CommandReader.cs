using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 指令读取
    /// </summary>
    public class CommandReader
    {
        /// <summary>
        /// 解析后的指令集
        /// </summary>
        private static List<TAClientCommand> Commands { get; set; }

        /// <summary>
        /// 是否开始运行
        /// </summary>
        private static bool isRun = false;

        /// <summary>
        /// 开始读取
        /// </summary>
        public static void StartRead()
        {
            if (isRun)
            {
                return;
            }

            isRun = true;
            if (Commands == null)
            {
                Commands = new List<TAClientCommand>();
            }
            TAClientHelp.StartRecivce();

            Task.Factory.StartNew(() =>
            {
                do
                {
                    System.Threading.Thread.Sleep(1);
                    var messages = TAClientHelp.GetMessages();

                    if (messages != null && messages.Length > 0)
                    {
                        messages.ToList().ForEach(message =>
                        {
                            TAClientCommand tAClientCommand = new TAClientCommand()
                            {
                                CommandString = message
                            };
                            tAClientCommand.Analysis();
                            lock (Commands)
                            {
                                Commands.Add(tAClientCommand);
                            }
                        });
                    }
                } while (isRun);
            });
        }

        /// <summary>
        /// 停止读取
        /// </summary>
        public static void StopRead()
        {
            isRun = false;
            TAClientHelp.StopRecivce();
        }
    }
}
