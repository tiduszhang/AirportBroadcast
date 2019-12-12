using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.PlayAudio.Dto
{
    
    public class WavFileDto : EntityDto<long>
    {
        /// <summary>
        /// 返回标识 1成功，0失败
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// 生成语音文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 合成语音时长
        /// </summary>
        public double WavLength { get; set; }

    }
}
