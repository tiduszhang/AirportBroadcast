using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 指令接口
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual string Level { get; set; }

        /// <summary>
        /// 指令类型
        /// </summary>
        public virtual CommandType CommandType { get; set; }

        /// <summary>
        /// 指令编号
        /// </summary>
        public virtual string CommandNo { get; set; }

        /// <summary>
        /// 指令中的数据
        /// </summary>
        public virtual dynamic CommandData { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            try
            {
                var head = CommandString.Substring(0, 9);
                Level = head.Substring(0, 1);
                CommandNo = head.Substring(1, 5);

                var type = CommandString.Substring(6, 3);
                CommandType = (CommandType)Enum.Parse(typeof(CommandType), type);

                switch (CommandType)//根据不同的类型选择不同的指令解析
                {
                    case CommandType.AFL:
                        CommandData = new AFLCommand();
                        break;
                    case CommandType.ABD:
                        CommandData = new ABDCommand();
                        break;
                    case CommandType.FIL:
                        CommandData = new FILCommand();
                        break;
                    case CommandType.NTI:
                        CommandData = new NTICommand();
                        break;
                    case CommandType.UFL:
                        CommandData = new UFLCommand();
                        break;
                    case CommandType.IFL:
                        CommandData = new IFLCommand();
                        break;
                    case CommandType.DFL:
                        CommandData = new DFLCommand();
                        break;
                    case CommandType.USR:
                        CommandData = new USRCommand();
                        break;
                    case CommandType.ISR:
                        CommandData = new ISRCommand();
                        break;
                    case CommandType.DSR:
                        CommandData = new DSRCommand();
                        break;
                    case CommandType.URG:
                        CommandData = new URGCommand();
                        break;
                    case CommandType.CKN:
                        CommandData = new CKNCommand();
                        break;
                    default:
                        CommandData = new Command(); //非法指令不解析直接输出
                        break;
                }
                if (CommandData is Command)
                {
                    Message = String.Format("非法指令：【{0}】", CommandString);
                }
                else
                {
                    CommandData.CommandString = CommandString;

                    CommandData.Analysis();
                    Message = String.Format("正确指令：【{0}】", CommandString); ;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Message = String.Format("非法指令：【{0}】", CommandString); ;
            }
        }

        /// <summary>
        /// 字符串转换方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //级别+编号+命令+数据
            CommandString = Level + CommandNo + Enum.GetName(typeof(CommandType), CommandType) + CommandData.ToString();
            return CommandString;
        }
    }
}
