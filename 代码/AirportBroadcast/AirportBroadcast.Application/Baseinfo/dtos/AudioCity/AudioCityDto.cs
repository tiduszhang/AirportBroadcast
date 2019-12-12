using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo;

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 城市
    /// </summary> 
    public class AudioCityDto : BaseAudioEntityDto
    {
        /// <summary>
        /// 编号 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
    }
}
