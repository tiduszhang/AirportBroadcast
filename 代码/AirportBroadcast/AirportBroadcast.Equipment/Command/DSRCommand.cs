using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 删除航班服务
    /// </summary>
    public class DSRCommand
    {
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

            //解析定位条件 ：
            //FLNU，服务所属航班的URNO（不足10位左边补空格） 
            //FLNO，所属航班的航班号（不足9位左边补空格）
            //STOA或STOD，所属航班的计划时间  14位 
            //进出港标志 A 表示进港，D表示出港 1位 
            //SRNU，服务类型 具体服务有值机服务、登机服务、上轮档服务 5位
            var condition = data.Substring(0, 10 + 9 + 14 + 1 + 5);

            FlightServer = new FlightServer();
            FlightServer.FLNU = condition.Substring(0, 10).Trim();//航班的URNO 10位
            FlightServer.FLNO = condition.Substring(10, 9).Trim();//FLNO 航班号 9位 
            FlightServer.AORD = condition.Substring(10 + 9 + 14, 1);//A 表示进港，D表示出港 1位
            FlightServer.SRNU = condition.Substring(10 + 9 + 14 + 1, 5);//服务类型 5 位

            var time = condition.Substring(10 + 9, 14);// STOA或STOD，所属航班的计划时间  14位  YYYYMMDDhhmmss 

            if (FlightServer.AORD == "A")
            {
                FlightServer.STOA = time;
            }
            else
            {
                FlightServer.STOD = time;
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
