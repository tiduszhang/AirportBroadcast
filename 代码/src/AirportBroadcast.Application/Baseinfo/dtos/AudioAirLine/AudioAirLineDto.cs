using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo;

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 航空公司代码
    /// </summary>
     
    public class AudioAirLineDto : BaseAudioEntityDto
    {
        /// <summary>
        /// 编号（航空公司代码前两位）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
