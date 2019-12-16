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
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var head = CommandString.Substring(0, 9);
            Level = head.Substring(0, 1);
            CommandNo = head.Substring(1, 5);

            var type = CommandString.Substring(6, 3);
            CommandType = (CommandType)Enum.Parse(typeof(CommandType), type);

            switch (CommandType)//根据不同的类型选择不同的指令解析
            {
                case CommandType.AFL:
                    CommandData = new AFLCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.ABD:
                    CommandData = new ABDCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.FIL:
                    CommandData = new FILCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.NTI:
                    CommandData = new NTICommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.UFL:
                    CommandData = new UFLCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.IFL:
                    CommandData = new IFLCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.DFL:
                    CommandData = new DFLCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.USR:
                    CommandData = new USRCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.ISR:
                    CommandData = new ISRCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.DSR:
                    CommandData = new DSRCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.URG:
                    CommandData = new URGCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                case CommandType.CKN:
                    CommandData = new CKNCommand
                    {
                        CommandString = CommandString
                    };
                    CommandData.Analysis();
                    break;
                default:
                    break;
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
