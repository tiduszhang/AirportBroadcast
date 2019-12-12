using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    public class PlayAudioLog : CreationAuditedEntity<long>
    {
        public string FileName { get; set; }

        public string Remark { get; set; }
    }
}
