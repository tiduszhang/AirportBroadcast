using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.activeMq
{
    public class CommAudioFileName : Entity<long>
    {
        public long? AirshowDataId { get; set; }

        [ForeignKey("AirshowDataId")]
        public virtual AirshowData AirshowData { get; set; }

        public string FileName { get; set; }

        public int FileTotalTime { get; set; }

        public string Remark { get; set; }

        public PlayStatus PlayStatus { get; set; }

        /// <summary>
        /// 播放区域 00000000 （8位，0表示关，1表示开）
        /// </summary>
        public string PlayPort { get; set; }

        /// <summary>
        /// 进出港标记
        /// </summary>
        public string DeporArrCode { get; set; }

        public DateTime? StartPlayTime { get; set; }

        public DateTime? EndPlayTime { get; set; }

        public int TotalPlayTime { get; set; }

        public DateTime? WaitForPlay { get; set; }

        public DateTime CreationTime { get; set; }

    }

    public enum PlayStatus
    {
        暂未播放=0,
        等待播放,
        开始播放,
        播放中,
        暂停中,
        播放完成
    }
}
