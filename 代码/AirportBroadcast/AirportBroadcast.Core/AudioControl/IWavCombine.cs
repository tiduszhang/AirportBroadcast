using AirportBroadcast.Domain.wavFileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.AudioControl
{
    public interface IWavCombine
    {
        /// <summary>
        /// 语音小文件组合
        /// </summary>
        /// <param name="fullFileName">要组合的小文件集合</param>
        /// <returns></returns>
        WavFileData Concatenate(List<string> fullFileName,string remark);

        /// <summary>
        /// 语音文件播放
        /// </summary>
        /// <param name="fullFileName">播放语音文件地址</param>
        /// <param name="playerDevice">播放声卡</param>
        /// <param name="ports">播放端口 播放端口 00011001 表示4.5.8端口打开</param>
        /// <param name="priority">优先级 数值越大 优先级越高</param>
        void Player(string fullFileName, string playerDevice, string ports, int priority = 0);

        /// <summary>
        /// 播放状态事件
        /// </summary>
        event Action<PlayStateData> PlayStateCallback;

        /// <summary>
        /// 清除播放队列
        /// </summary>
        /// <returns></returns>
        bool ClearPlayQuenue();

        /// <summary>
        /// 人工插播  其余队列全部清空
        /// </summary>
        void Art_Player(string fullFileName, string playerDevice, string ports);

        void Art_PlayerTxt(string fullFileName, string playerDevice, string ports);
        /// <summary>
        /// 获取可用声卡列表
        /// </summary>
        /// <returns></returns>
        List<string> GetVoiceDevice();
    }
}
