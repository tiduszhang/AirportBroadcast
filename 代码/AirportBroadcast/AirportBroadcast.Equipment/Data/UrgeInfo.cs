using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 催促信息
    /// </summary>
    public class UrgeInfo
    {
        /// <summary>
        /// 航班的URNO
        /// </summary>
        public string URNO { get; set; }

        /// <summary>
        /// 航班号FLNO （不足9位左边补空格）
        /// </summary>
        public string FLNO { get; set; }

        /// <summary>
        /// 航班计划出发时间STOD
        /// </summary>
        public string STOD { get; set; }

        /// <summary>
        /// AORD，航班的进出港标志，本消息始终是D
        /// </summary>
        public string AORD { get; set; }

        /// <summary>
        /// 催促时间，登机门号做Key，时间做Value
        /// </summary>
        public Dictionary<string, string> UrgeDatas { get; set; } 
    }
}
