using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo;

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 常用短语
    /// </summary> 
    public class AudioConstDto : BaseAudioEntityDto
    {
        /// <summary>
        /// 语音使用场景编号
        /// </summary>
        public string ConstNo { get; set; }

        /// <summary>
        /// 语音使用场景说明
        /// </summary>
        public string Remark { get; set; }
    }
}
