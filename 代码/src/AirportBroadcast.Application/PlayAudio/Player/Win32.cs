//13574172409  ������
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
        /// �ͷ���Դ
        /// </summary>
        /// <remarks>�ͷŵ�ǰ��������Դ</remarks>
        /// <returns>0:�ɹ���������ʧ��</returns>
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
        /// �ر�����
        /// </summary>
        /// <remarks>�رյ�ǰ���ŵ�������</remarks>
        /// <param name="PlayerID">�������</param>
        /// <returns>0:�ɹ���������ʧ��</returns>
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
        /// ��ȡ�����б�
        /// </summary>
        /// <remarks>ֱ�ӷ��أ��Իس����зָ���</remarks>
        /// <returns>�����б�</returns>
        [DllImport("MSndCard.dll")]
        public static extern string  MC_GetDeviceListVB() ;
    }
}
