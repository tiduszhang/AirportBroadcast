using System;
using System.Collections.Generic;
using System.Text;
using System.Timers ;
using System.IO;
namespace AirportBroadcast.PlayAudio
{
    public class Audio : AirportBroadcast.PlayAudio.IAudio
    {
        string path;
        int PlayerNum;  //�ڼ���������
        int States=0;   //����״̬
        Timer timer = new Timer(1000);
        int left=0;     //������
        int right=0;    //������
        int len=0;      //����
        byte DeviceListID = 0;//�ڼ�������
        int position=0;//��ǰλ��
        int ports=0;    //����˿�
        public delegate void EndofPlayEventHandle(int ports, int PlayerNum); //���Ž���
        public event EndofPlayEventHandle EndofPlayEvent;
        public delegate void StatusofPlayEventHandle(int playState, int position, int len);//��ȡ״̬
        public event StatusofPlayEventHandle StatusPlayEvent;
        #region
        /// <summary>
        /// �豸���
        /// </summary>
        /// <remarks>��ǰ���������������</remarks>
        /// <value>����</value>
        public byte DeviceId
        {
            set { this.DeviceListID = value; }
            get { return this.DeviceListID; }
        }
        public int PlayState
        {
            get { return this.States; }
        }
        public int Left
        {
            get { return this.left; }
        }
        public int Right
        {
            get { return this.right; }
        }
        public int Duration
        {
            get { return this.len; }
        }
        public int Position
        {
            get { return position; }
        }
        /// <summary>
        /// �����б�
        /// </summary>
        /// <value>��\r\n�ָ����ı�</value>
        public string DeviceList
        {
            get { return Win32.MC_GetDeviceListVB(); }
        }
        #endregion
        public Audio()
        { 
        
        }
        public Audio(int PlayerNum)
        {
            this.PlayerNum = PlayerNum;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }
        public Audio(int PlayerNum, byte  DeviceListID)
        {
            this.PlayerNum = PlayerNum;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            this.DeviceListID = DeviceListID;
            
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            left    = Win32.MC_GetVU(PlayerNum, 0);
            right   = Win32.MC_GetVU(PlayerNum, 1);
            len     = Win32.MC_GetFileDuration(PlayerNum);
            position = Win32.MC_GetPosition(PlayerNum);
            States  = Win32.MC_GetStates(PlayerNum);
            if (this.StatusPlayEvent != null)
            {
                this.StatusPlayEvent(States, position, len);
            }
            if (  States == 5 )   //�������
            {
                timer.Enabled = false;
                if (this.EndofPlayEvent != null)
                {
                    Win32.MC_Close(DeviceId);
                    try
                    {
                        File.Delete(this.path);
                    }
                    catch { }
                    this.EndofPlayEvent(this.ports, PlayerNum);
                }

            }
        }
        //public int Init()
        //{
        //   return  Win32.MC_Init(2);
        //}
        public int play(string path,int ports)
        { 
        //MC_GetStates(0)
            this.path = path;
            int tmp;
            this.ports = ports;
            if (States == 0 || States == 1 || States == 5)   //���Բ���
            {
              Win32.MC_SelectDevice(PlayerNum, DeviceListID);
              Win32.MC_SetFileName(PlayerNum, path);
                if ((tmp  =Win32.MC_Open(PlayerNum))==0) //��ʼ����
                {
                    timer.Enabled = true;
                   return  Win32.MC_Play(PlayerNum);
                }
               else 
                    return -1;
            }
            else
            {
                return States;
            }
        }
        public void stop()
        {
            Win32.MC_Stop(PlayerNum); //ֹͣ
        }
        public void Pause()
        {
            Win32.MC_Pause(PlayerNum);//��ͣ
        }
    }
}
