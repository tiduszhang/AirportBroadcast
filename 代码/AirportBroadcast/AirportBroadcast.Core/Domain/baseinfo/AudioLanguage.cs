using Abp.Domain.Entities.Auditing;

namespace AirportBroadcast.Domain.baseinfo
{
    /// <summary>
    /// 语种
    /// </summary>
    public class AudioLanguage : FullAuditedEntity
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
