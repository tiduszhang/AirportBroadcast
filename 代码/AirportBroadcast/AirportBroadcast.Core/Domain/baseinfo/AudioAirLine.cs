using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 航空公司代码
    /// </summary>
    public class AudioAirLine:BaseAudioEntity
    {
        /// <summary>
        /// 编号（航空公司代码前两位）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
