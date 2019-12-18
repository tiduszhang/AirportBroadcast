using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 删除航班
    /// </summary>
    public class DFLCommand
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
        /// 航班URNO
        /// </summary>
        public virtual string URNO { get; set; }
        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部
            URNO = data.Trim(); // 航班URNO
        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }


        /// <summary>
        /// 创建MQ消息体
        /// </summary>
        public virtual string CreateMQCommand()
        {
            AirShowData airShowData = new AirShowData();

            airShowData.RouteType = "DELETE";//关键字标识 MODIFY 修改
            airShowData.LocalAirportCode = "LUM";//本地机场标识

            airShowData.FlightNo2 = this.URNO;//航班号

            return Newtonsoft.Json.JsonConvert.SerializeObject(airShowData);
        }
    }
}
