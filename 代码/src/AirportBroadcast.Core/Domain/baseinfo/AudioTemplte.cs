using Abp.Domain.Entities.Auditing;
using AirportBroadcast.Domain.playSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 语句模板
    /// </summary>
    public class AudioTemplte:FullAuditedEntity
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
        public virtual int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public virtual AudioLanguage AudioLanguage { get; set; }

        /// <summary>
        /// 语句段明细
        /// </summary>
        public virtual List<AudioTemplteDetail> Details { get; set; }

         
    }
}
