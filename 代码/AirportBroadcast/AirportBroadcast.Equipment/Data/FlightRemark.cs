using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航班备注
    /// </summary>
    public class FlightRemark
    {
        #region RM航班备注基础数据字段

        /// <summary>
        /// 航班备注字母代码
        /// </summary>
        public string DECA { get; set; }

        /// <summary>
        /// 航班备注数字代码
        /// </summary>
        public string DECN { get; set; }

        /// <summary>
        /// 航班备注信息
        /// </summary>
        public string BDLA { get; set; }

        /// <summary>
        /// 航班备注信息(英文)
        /// </summary>
        public string BDLE { get; set; }

        #endregion

    }
}
