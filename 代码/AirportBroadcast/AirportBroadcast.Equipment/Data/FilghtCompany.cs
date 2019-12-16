using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航空公司
    /// </summary>
    public class FilghtCompany
    {
        #region AL航空公司基础数据字段

        /// <summary>
        /// 航空公司二字代码
        /// </summary>
        public string ALC2 { get; set; }

        /// <summary>
        /// 航空公司三字代码
        /// </summary>
        public string ALC3 { get; set; }

        /// <summary>
        /// 航空公司中文名称
        /// </summary>
        public string ALNC { get; set; }

        /// <summary>
        /// 航空公司英文名称
        /// </summary>
        public string ALNE { get; set; }

        /// <summary>
        /// 航空公司国际、国内属性
        /// </summary>
        public string DORI { get; set; }

        #endregion

    }
}
