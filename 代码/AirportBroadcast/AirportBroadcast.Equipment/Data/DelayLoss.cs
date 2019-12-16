using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 延误代码
    /// </summary>
    public class DelayLoss
    {
        #region DL延误代码基础数据字段

        /// <summary>
        /// 延误字母代码
        /// </summary>
        public string DECA { get; set; }

        /// <summary>
        /// 延误数字代码
        /// </summary>
        public string DECN { get; set; }

        /// <summary>
        /// 延误原因描述
        /// </summary>
        public string BDLA { get; set; }

        /// <summary>
        /// 延误原因描述(英文)
        /// </summary>
        public string BDLE { get; set; }

        #endregion

    }
}
