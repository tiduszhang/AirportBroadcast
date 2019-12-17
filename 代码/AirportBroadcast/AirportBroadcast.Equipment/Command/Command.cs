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
        /// 结果指令
        /// </summary>
        public Command ResaultCommand { get; set; }

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
                        Message = String.Format("查询航班信息指令：【{0}】", CommandString);
                        break;
                    case CommandType.ABD:
                        CommandData = new ABDCommand();
                        Message = String.Format("查询基础数据指令：【{0}】", CommandString);
                        break;
                    case CommandType.FIL:
                        CommandData = new FILCommand();
                        Message = String.Format("返回查询结果所存的文件名(用于批量航班查询反馈)指令：【{0}】", CommandString);
                        break;
                    case CommandType.NTI:
                        CommandData = new NTICommand();
                        Message = String.Format("通知取文件(航班计划文件)指令：【{0}】", CommandString);
                        break;
                    case CommandType.UFL:
                        CommandData = new UFLCommand();
                        Message = String.Format("更新航班指令：【{0}】", CommandString);
                        break;
                    case CommandType.IFL:
                        CommandData = new IFLCommand();
                        Message = String.Format("新增航班指令：【{0}】", CommandString);
                        break;
                    case CommandType.DFL:
                        CommandData = new DFLCommand();
                        Message = String.Format("删除航班指令：【{0}】", CommandString);
                        break;
                    case CommandType.USR:
                        CommandData = new USRCommand();
                        Message = String.Format("更新航班服务指令：【{0}】", CommandString);
                        break;
                    case CommandType.ISR:
                        CommandData = new ISRCommand();
                        Message = String.Format("新增航班服务指令：【{0}】", CommandString);
                        break;
                    case CommandType.DSR:
                        CommandData = new DSRCommand();
                        Message = String.Format("删除航班服务指令：【{0}】", CommandString);
                        break;
                    case CommandType.URG:
                        CommandData = new URGCommand();
                        Message = String.Format("催促登机指令：【{0}】", CommandString);
                        break;
                    case CommandType.CKN:
                        CommandData = new CKNCommand();
                        Message = String.Format("重新值机（重新办理乘机手续）指令------------- 暂不使用，这个指令应该不存在 ：【{0}】", CommandString);
                        break;
                    default:
                        //CommandData = new Command(); //非法指令不解析直接输出
                        Message = String.Format("非法指令不解析 ：【{0}】", CommandString);
                        break;
                }
                if (CommandData != null)
                {
                    CommandData.CommandString = CommandString;
                    CommandData.Analysis();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Message = String.Format("非法指令不解析：【{0}】", CommandString); ;
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        public void Read()
        {
            try
            {
                ResaultCommand.CommandData.Type = CommandData.Type;
                ResaultCommand.CommandData.Read();
            }
            catch (Exception ex)
            {
                ex.ToString();
                Message = "指令解析异常";
            }
        }

        /// <summary>
        /// 字符串转换方法
        /// </summary>
        /// <returns></returns>
        public string ToCommandString()
        {
            //级别+编号+命令+数据
            CommandString = Level + CommandNo.PadLeft(5, '0') + Enum.GetName(typeof(CommandType), CommandType) + CommandData.ToString();
            return CommandString;
        }

    }
}
