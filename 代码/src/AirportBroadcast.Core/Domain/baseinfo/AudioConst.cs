using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 常用短语
    /// </summary>
    public class AudioConst : BaseAudioEntity
    {
        /// <summary>
        /// 语音使用场景编号
        /// </summary>
        public string ConstNo { get; set; }

        /// <summary>
        /// 语音使用场景说明
        /// </summary>
        public string Remark { get; set; }
    }
}
