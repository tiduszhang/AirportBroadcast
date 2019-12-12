using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    ///登记门
    /// </summary>
    public class AudioGate : BaseAudioEntity
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
