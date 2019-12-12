using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 语音文件基类
    /// </summary>
    public class BaseAudioEntity : FullAuditedEntity
    {
        /// <summary>
        /// 文件名称（xxx.wav）
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件所在目录（D:/xxx/xxx/）
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 语音文件对应文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 语音文件对应文字说明（汉语翻译）
        /// </summary>
        public string ContentRemark { get; set; }

        /// <summary>
        /// 语音文件对应的语种
        /// </summary>
        public virtual int LanguageId { get; set; }

       
        [ForeignKey("LanguageId")]
        public virtual AudioLanguage AudioLanguage { get; set; }
    }
}
