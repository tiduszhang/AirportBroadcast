using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 返回查询结果所存的文件名(用于批量航班查询反馈)
    /// </summary>
    public class FILCommand
    {
        /// <summary>
        /// 指令等级
        /// </summary>
        public readonly string CommandLevel = "S";

        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }

        /// <summary>
        /// AP机场基础数据
        /// </summary>
        public virtual Airport Airport { get; set; }

        /// <summary>
        /// 子命令类型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {

        }
        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }

        /// <summary>
        /// 读取信息
        /// </summary>
        public virtual void Read()
        {
            switch (this.Type)
            {
                case ABDCommand.AL:
                    ReadAL();
                    break;
                case ABDCommand.AP:
                    ReadAP();
                    break;
                case ABDCommand.DL:
                    ReadDL();
                    break;
                case ABDCommand.NA:
                    ReadNA();
                    break;
                case ABDCommand.RM:
                    ReadRM();
                    break;
                case ABDCommand.SV:
                    ReadSV();
                    break;
                default:
                    ReadDefault();
                    break;
            }
        }

        /// <summary>
        /// 读取航班信息数据
        /// </summary>
        public virtual void ReadDefault()
        {

        }

        /// <summary>
        /// 读取AP机场基础数据
        /// </summary>
        public virtual void ReadAP()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

            Airport = new Airport();

            var fieldValues = data.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            var type = Airport.GetType();

            var i = 0;
            foreach (var fieldValue in fieldValues)
            {
                var temp = fieldValue.Split('=');
                var field = temp[0];//属性名称
                var value = temp[1];//属性对应的值
                var property = type.GetProperty(field);
                if (property != null)
                {
                    i++;
                    property.SetValue(Airport, value);
                }
            }
        }

        /// <summary>
        /// 读取AL航空公司基础数据
        /// </summary>
        public virtual void ReadAL()
        {

        }

        /// <summary>
        /// 读取SV航班服务基础数据
        /// </summary>
        public virtual void ReadSV()
        {

        }

        /// <summary>
        /// 读取RM航班备注基础数据
        /// </summary>
        public virtual void ReadRM()
        {

        }

        /// <summary>
        /// 读取DL延误代码基础数据
        /// </summary>
        public virtual void ReadDL()
        {

        }

        /// <summary>
        /// 读取NA航班性质基础数据
        /// </summary>
        public virtual void ReadNA()
        {

        }
    }
}
