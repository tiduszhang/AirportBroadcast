using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.wavFileInfo
{
    public class WavFileData : Entity<long>
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
