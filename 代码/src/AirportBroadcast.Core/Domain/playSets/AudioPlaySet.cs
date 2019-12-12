using Abp.Domain.Entities.Auditing;
using AirportBroadcast.Domain.baseinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.playSets
{
    public class AudioPlaySet : FullAuditedEntity
    {
        /// <summary>
        /// 航班扭转状态
        /// </summary>
        public string Code { get; set; }

        public bool AutoPlay { get; set; }

        public virtual List<AudioPlaySetTemple> Templtes { get; set; }

        public virtual List<AudioDevice> AudioDevices { get; set; }

       

        /// <summary>
        /// 国内
        /// </summary>
        public virtual List<TopPwrPort> CnTopPwrPorts { get; set; }

        /// <summary>
        /// 国际
        /// </summary>
        public virtual List<TopPwrPort> EnTopPwrPorts { get; set; }

        public string Remark { get; set; }
    }
}
