//13574172409  方经理
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
/**/
namespace AirportBroadcast.PlayAudio
{
   public  class Win32
    {
        [DllImport("MSndCard.dll")]
        public static extern int MC_Init (int PlayerNum);
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <remarks>释放当前播放器资源</remarks>
        /// <returns>0:成功　其他：失败</returns>
        [DllImport("MSndCard.dll")]
        public static extern int MC_Free() ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_SetFileName(int PlayerID, string FileName) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_Open(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_Play(int PlayerID);
        [DllImport("MSndCard.dll")]
        public static extern int MC_Pause(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_Stop(int PlayerID);
        /// <summary>
        /// 关闭声卡
        /// </summary>
        /// <remarks>关闭当前播放的声卡。</remarks>
        /// <param name="PlayerID">声卡编号</param>
        /// <returns>0:成功　其他：失败</returns>
        [DllImport("MSndCard.dll")]
        public static extern int MC_Close(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_GetStates(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_GetFileDuration(int PlayerID);
        [DllImport("MSndCard.dll")]
        public static extern int MC_GetPosition(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_SetPosition(int PlayerID ,long Value) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_GetVolume(int PlayerID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_SetVolume(int PlayerID, byte Value) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_SetPan(int PlayerID, byte PanID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_GetVU(int PlayerID, byte ChannelID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_SelectDevice(int PlayerID,byte DeviceListID) ;
        [DllImport("MSndCard.dll")]
        public static extern int MC_RegistryFilter(string CodecsPath);
        [DllImport("MSndCard.dll")]
        public static extern int MC_SetSN(string SN);
        /// <summary>
        /// 获取声卡列表
        /// </summary>
        /// <remarks>直接返回，以回车换行分隔。</remarks>
        /// <returns>声卡列表</returns>
        [DllImport("MSndCard.dll")]
        public static extern string  MC_GetDeviceListVB() ;
    }
}
