using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航班服务
    /// </summary>
    public class FlightServer
    {
        #region 对应于航班服务的相关字段及适用命令

        /// <summary>
        /// 服务类型
        /// </summary>
        public string SRNU { get; set; }

        /// <summary>
        /// 所属航班的URNO
        /// </summary>
        public string FLNU { get; set; }

        /// <summary>
        /// 服务实际开始时间
        /// </summary>
        public string SRAB { get; set; }

        /// <summary>
        /// 服务实际结束时间
        /// </summary>
        public string SRAE { get; set; }

        /// <summary>
        /// 服务计划开始时间
        /// </summary>
        public string SRSB { get; set; }

        /// <summary>
        /// 服务计划结束时间
        /// </summary>
        public string SRSE { get; set; }

        /// <summary>
        /// 服务备注信息
        /// </summary>
        public string REMK { get; set; }


        #endregion

        #region 航班对应属性

        /// <summary>
        /// FLNO，所属航班的航班号（不足9位左边补空格）
        /// </summary>
        public string FLNO { get; set; }
        /// <summary>
        /// AORD，所属航班的进出港标志  A 表示进港，D表示出港 1位 
        /// </summary>
        public string AORD { get; set; }
        /// <summary>
        /// STOA或STOD，所属航班的计划时间  A 表示进港，D表示出港 1位 
        /// </summary>
        public string STOA { get; set; }
        /// <summary>
        /// STOA或STOD，所属航班的计划时间  A 表示进港，D表示出港 1位 
        /// </summary>
        public string STOD { get; set; }

        #endregion
    }
}
