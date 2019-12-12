using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.playSets
{
    /// <summary>
    /// 声卡
    /// </summary>
    public class AudioDevice : Entity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        public virtual List<AudioPlaySet> AudioPlaySets { get; set; }
    }
}
