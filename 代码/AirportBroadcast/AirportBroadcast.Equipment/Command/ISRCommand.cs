using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 新增航班服务
    /// </summary>
    public class ISRCommand
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
        /// 航班服务信息
        /// </summary>
        public virtual FlightServer FlightServer { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

            FlightServer = new FlightServer();
             
            var fieldValues = data.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            var type = FlightServer.GetType();

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
                    property.SetValue(FlightServer, value);
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
