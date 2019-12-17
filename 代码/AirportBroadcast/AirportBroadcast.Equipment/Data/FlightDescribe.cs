using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// SV航班服务基础数据
    /// </summary>
    public class FlightDescribe
    {
        #region SV航班服务基础数据字段

        /// <summary>
        /// 服务代码
        /// </summary>
        public string SRNU { get; set; }

        /// <summary>
        /// 服务中文描述
        /// </summary>
        public string SRDC { get; set; }

        /// <summary>
        /// 服务英文描述
        /// </summary>
        public string SRDE { get; set; }

        #endregion

    }
}
