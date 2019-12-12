using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 语种
    /// </summary>
    [AutoMap(typeof(AudioLanguage))]
    public class AudioLanguageDto : FullAuditedEntityDto
    {
        /// <summary>
        /// 语种编号
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 语种名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 语种说明
        /// </summary>
        public string Remark { get; set; }
    }
}
