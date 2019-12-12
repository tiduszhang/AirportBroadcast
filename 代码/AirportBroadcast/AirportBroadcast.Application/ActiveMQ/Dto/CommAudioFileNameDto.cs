using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using AirportBroadcast.Domain.activeMq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    [AutoMap(typeof(CommAudioFileName))]
    public class CommAudioFileNameDto : EntityDto<long>
    {
        public long AirshowDataId { get; set; }
         
        public string FileName { get; set; }

        public int FileTotalTime { get; set; }

        public string Remark { get; set; }

        public PlayStatus PlayStatus { get; set; }

        public DateTime? StartPlayTime { get; set; }

        public DateTime? EndPlayTime { get; set; }

        public int TotalPlayTime { get; set; }

    }

    
}
