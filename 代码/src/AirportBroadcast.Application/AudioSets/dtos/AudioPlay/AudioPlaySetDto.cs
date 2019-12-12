using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.AudioSets.dtos
{
    public class AudioPlaySetDto : FullAuditedEntityDto
    {
        /// <summary>
        /// 航班扭转状态
        /// </summary>
        public string Code { get; set; }

        public bool AutoPlay { get; set; }

        public string Remark { get; set; }

    }
}
