using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.AudioSets.dtos
{
    /// <summary>
    /// 声卡
    /// </summary>
    public class AudioDeviceDto : EntityDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
         
    }
}
