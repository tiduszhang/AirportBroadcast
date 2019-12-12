using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo;
using System.Collections.Generic; 

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 语句模板带明细
    /// </summary>    
    public class AudioTemplteDto : AudioTemplteBaseDto
    {        
        /// <summary>
        /// 语句段明细
        /// </summary>
        public virtual List<AudioTemplteDetailDto> Details { get; set; }

    }

    /// <summary>
    /// 语句模板
    /// </summary>
    public class AudioTemplteBaseDto : FullAuditedEntityDto
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 语音文件对应的语种
        /// </summary>
        public int LanguageId { get; set; }


        public string AudioLanguageName { get; set; }

       
    }


}
