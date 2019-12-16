using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 机场
    /// </summary>
    public class Airport
    {
        #region AP机场基础数据字段

        /// <summary>
        /// 机场三字代码
        /// </summary>
        public string APC3 { get; set; }

        /// <summary>
        /// 机场四字代码
        /// </summary>
        public string APC4 { get; set; }

        /// <summary>
        /// 机场中文名称
        /// </summary>
        public string APNC { get; set; }

        /// <summary>
        /// 机场英文名称
        /// </summary>
        public string APNE { get; set; }

        /// <summary>
        /// 机场中文简称
        /// </summary>
        public string APN1 { get; set; }

        /// <summary>
        /// 机场英文简称
        /// </summary>
        public string APN2 { get; set; }

        /// <summary>
        /// 机场国际、国内属性
        /// </summary>
        public string DORI { get; set; }

        #endregion

    }
}
