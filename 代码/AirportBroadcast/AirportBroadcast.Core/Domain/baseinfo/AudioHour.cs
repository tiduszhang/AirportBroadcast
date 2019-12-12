using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 小时
    /// </summary>
    public class AudioHour : BaseAudioEntity
    {
        /// <summary>
        /// 编号 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
