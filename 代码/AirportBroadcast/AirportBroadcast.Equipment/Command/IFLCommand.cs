using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 新增航班
    /// </summary>
    public class IFLCommand
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
        /// 航班信息
        /// </summary>
        public virtual FlightInfo FlightInfo { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

            var fieldValues = data.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            FlightInfo = new FlightInfo();
            var type = FlightInfo.GetType();

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
                    property.SetValue(FlightInfo, value);
                }
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
