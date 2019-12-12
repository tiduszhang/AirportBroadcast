using Abp.Domain.Entities.Auditing;
using AirportBroadcast.Domain.baseinfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.playSets
{
    public class AudioPlaySetTemple : FullAuditedEntity
    {

        public virtual int TempleId { get; set; }

        [ForeignKey("TempleId")]
        public virtual AudioTemplte AudioTemplte { get; set; }


        public virtual int PlayId { get; set; }

        [ForeignKey("PlayId")]
        public virtual AudioPlaySet AudioPlaySet { get; set; }

        public int Sort { get; set; }
    }
}
