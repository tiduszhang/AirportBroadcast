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
    public class CommandHelp
    {
        /// <summary>
        /// 航显主动推送解析后的指令集
        /// </summary>
        private static List<Command> Commands { get; set; }

        /// <summary>
        /// 请求航显数据指令集
        /// </summary>
        private static List<Command> SendCommands { get; set; }

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

            if (SendCommands == null)
            {
                SendCommands = new List<Command>();
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
                                if (command.CommandType == CommandType.FIL)
                                {
                                    lock (SendCommands)
                                    {
                                        var sendComm = SendCommands.FirstOrDefault(sendCommand => sendCommand.CommandNo == command.CommandNo);
                                        if (sendComm != null)
                                        {
                                            sendComm.ResaultCommand = command;
                                        }
                                    }
                                }
                                else
                                {
                                    lock (Commands)
                                    {
                                        Commands.Add(command);
                                    }
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
            if (command.CommandData == null)
            {
                return null;
            }
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
                return null;
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

        /// <summary>
        /// 获取发送的指令
        /// </summary>
        /// <returns></returns>
        public static Command[] GetSendCommands()
        {
            if (SendCommands == null)
            {
                return null;
            }
            Command[] commands = null;
            lock (SendCommands)
            {
                if (SendCommands.Count > 0)
                {
                    var sendCommands = SendCommands.Where(sendCommand => sendCommand.ResaultCommand != null).ToList();
                    commands = sendCommands.ToArray();
                    SendCommands.RemoveAll(sendCommand => sendCommand.ResaultCommand != null);
                    //sendComm.Read();//读取返回值
                }
            }
            return commands;
        }

        /// <summary>
        /// 发送指令数据
        /// </summary>
        /// <param name="command"></param>
        public static void SendCommand(Command command)
        {
            var commandNo = DateTime.Now.ToString().GetHashCode().ToString();
            command.CommandNo = commandNo.Length > 5 ? commandNo.Substring(0, 5) : commandNo;

            lock (SendCommands)
            {
                SendCommands.Add(command);
            }

            TAClientHelp.Send(command.ToCommandString());
        }
    }
}
