using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 数字
    /// </summary>
    public class AudioDigit:BaseAudioEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///说明
        /// </summary>
        public string Remark { get; set; }
    }
}
