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
        private static List<Command> Commands { get; set; }

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
                Commands = new List<Command>();
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
                            var command = Analysis(message);
                            if (command != null)
                            {
                                lock (Commands)
                                {
                                    Commands.Add(command);
                                }
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

        /// <summary>
        /// 解析指令
        /// </summary>
        /// <param name="commandString"></param>
        /// <returns></returns>
        private static Command Analysis(string commandString)
        {
            Command command = null;

            if (String.IsNullOrWhiteSpace(commandString))//过滤空值
            {
                return command;
            }
            command = new Command()//指令解析
            {
                CommandString = commandString
            };
            command.Analysis();
            return command;
        }

        /// <summary>
        /// 获取命令数据列表
        /// </summary>
        /// <returns></returns>
        public static Command[] GetCommands()
        {
            if (Commands == null)
            {
                lock (Commands)
                {
                    Commands = new List<Command>();
                }
            }
            Command[] commands = null;
            lock (Commands)
            {
                if (Commands.Count > 0)
                {
                    commands = Commands.ToArray();
                    Commands.Clear();
                }
            }
            return commands;
        }
    }
}
