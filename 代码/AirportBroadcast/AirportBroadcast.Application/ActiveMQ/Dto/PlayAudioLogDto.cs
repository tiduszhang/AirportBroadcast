using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    public class PlayAudioLogDto : CreationAuditedEntityDto<long>
    {
        public string FileName { get; set; }

        public string Remark { get; set; }
    }
}
