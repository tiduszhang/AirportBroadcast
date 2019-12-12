using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    public class HandPlayDto
    {
        /// <summary>
        /// 航显数据id
        /// </summary>
        public long Aid { get; set; }

        /// <summary>
        /// 操作命令
        ///值机：CKI ， CKOFF
        ///登机：BOR_N ， URBOR ，BOR_G，READY
        ///出港：DLY_C ， CAN_C 
        ///进港：EARR，ARR，FBAG，DLY_J，CAN_J
        /// </summary>
        public string PlayCommand { get; set; }

        /// <summary>
        /// 值机柜台Id
        /// </summary>
        public string CheckInCode { get; set; }

        /// <summary>
        /// 登机口 ID
        /// </summary>
        public string GateCode { get; set; }

        /// <summary>
        /// 行礼转盘 ID
        /// </summary>
        public string TurnPlateCode { get; set; }

        /// <summary>
        /// 延误/取消 航班原因ID
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// 预计 到达/起飞 时间，格式：HH:mm  例  19:30 或为空
        /// </summary>
        public string ArrOrDepTime { get; set; }


    }
}
