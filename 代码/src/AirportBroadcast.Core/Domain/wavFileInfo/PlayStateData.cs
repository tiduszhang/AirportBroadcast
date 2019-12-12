using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.wavFileInfo
{
    public class PlayStateData
    {

        /// <summary>
        /// 语音文件名（Guid.wav）
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 播放状态 0开始播放 1播放结束
        /// </summary>
        public int PlayState { get; set; }

    }
}
