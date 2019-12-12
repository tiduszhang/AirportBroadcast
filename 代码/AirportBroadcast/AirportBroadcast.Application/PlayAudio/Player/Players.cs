using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
/*player ���к�
 *2010-01-25�޸�ͨѶЭ��
 *  xing
 */
namespace AirportBroadcast.PlayAudio
{

    public class Players
    {
        Audio[] audio;

        bool[] p1 = new bool[8];//�˿�//����1
        bool[] p2 = new bool[8];//�˿�//����2
        byte[] Command = new byte[13];//����
        //�����˼̵�����Э����� ��������1200�ĳ�9600
        PortData portData = new PortData("COM1", 9600, Parity.None);
        int[] Arr_PortDetail = new int[8];
        public Audio[] Audios
        {
            get { return audio; }
        }
        #region  //����
        public int SetCom
        {
            set
            {
                switch (value)
                {
                    case 2:
                        portData = new PortData("COM2", 9600, Parity.None);
                        break;
                    case 3:
                        portData = new PortData("COM3", 9600, Parity.None);
                        break;
                    case 4:
                        portData = new PortData("COM4", 9600, Parity.None);
                        break;
                    case 5:
                        portData = new PortData("COM5", 9600, Parity.None);
                        break;
                    case 6:
                        portData = new PortData("COM6", 9600, Parity.None);
                        break;
                    default:
                        portData = new PortData("COM1", 9600, Parity.None);
                        break;
                }
            }
        }

        public Players(int PlayerCount)
        {
            for (int i = 0; i < 8; i++)
            {
                Arr_PortDetail[i] = 0;
            }
            if (PlayerCount > 0)
            {
                Win32.MC_Init(2);
                Win32.MC_SetSN("HN300-ffff4-DG4DE-FR2TG-fF4T6");
                if (!System.IO.Directory.Exists("tmp"))
                {
                    Directory.CreateDirectory("tmp");
                }
                foreach (string f in Directory.GetFiles("tmp"))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch
                    { }
                }
                audio = new Audio[PlayerCount];
                for (int t = 0; t < PlayerCount; t++)
                {
                    audio[t] = new Audio(t);
                    audio[t].DeviceId = (byte)t;
                    audio[t].EndofPlayEvent += new Audio.EndofPlayEventHandle(Players_EndofPlayEvent);
                }
            }
        }

        void Players_EndofPlayEvent(int ports, int PlayerNum)
        {
            SendCommand(false, PlayerNum);
            setport(ports, false, PlayerNum);
        }


        private void setport(int portS, bool flag, int PlayerNum)
        {
            if (PlayerNum == 0)
            {
                for (int index = 1; index < 9; index++)
                {
                    if ((portS & int.Parse(Math.Exp((index * Math.Log(2))).ToString())) != 0)
                    {
                        switch (index)
                        {
                            case 1:
                                p1[0] = flag;
                                break;
                            case 2:
                                p1[1] = flag;
                                break;
                            case 3:
                                p1[2] = flag;
                                break;
                            case 4:
                                p1[3] = flag;
                                break;
                            case 5:
                                p1[4] = flag;
                                break;
                            case 6:
                                p1[5] = flag;
                                break;
                            case 7:
                                p1[6] = flag;
                                break;
                            case 8:
                                p1[7] = flag;
                                break;
                        }
                    }
                }
            }
            else
            {
                for (int index = 1; index < 9; index++)
                {
                    if ((portS & int.Parse(Math.Exp((index * Math.Log(2))).ToString())) != 0)
                    {
                        switch (index)
                        {
                            case 1:
                                p2[0] = flag;
                                break;
                            case 2:
                                p2[1] = flag;
                                break;
                            case 3:
                                p2[2] = flag;
                                break;
                            case 4:
                                p2[3] = flag;
                                break;
                            case 5:
                                p2[4] = flag;
                                break;
                            case 6:
                                p2[5] = flag;
                                break;
                            case 7:
                                p2[6] = flag;
                                break;
                            case 8:
                                p2[7] = flag;
                                break;
                        }
                    }
                }
            }
        }


        //playflag true �� false ��
        #region ������Դ������  ͨѶЭ��
        private void SendCommand(bool playflag,  int PlayerNum)
       {
             //�򿪶˿�
            if (!portData.IsOpen())
            {
                portData.Open();
            }
                       
            if (PlayerNum == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p1[i])
                    {
                        //Э���ֽ�����
                        byte[] CloseCommand = new byte[5];
                        CloseCommand[0] = 0xFF;//��ʼ��
                        CloseCommand[1] = 0x00;//�豸��ַ�룬Ĭ��00
                         switch (i)
                        {
                            case 0:
                                CloseCommand[2] = 0x01;
                                break;
                            case 1:
                                CloseCommand[2] = 0x02;
                                break;
                            case 2:
                                CloseCommand[2] = 0x03;
                                break;
                            case 3:
                                CloseCommand[2] = 0x04;
                                break;
                            case 4:
                                CloseCommand[2] = 0x05;
                                break;
                            case 5:
                                CloseCommand[2] = 0x06;
                                break;
                            case 6:
                                CloseCommand[2] = 0x07;
                                break;
                            case 7:
                                CloseCommand[2] = 0x08;
                                break;
                        }
                        //0x01��ͨ0x00�ر�
                        if (playflag)   CloseCommand[3] = 0x01;
                        else CloseCommand[3] = 0x00;
                        //������
                        CloseCommand[4] = 0x55;
                        
                        portData.SendData(CloseCommand);
                        System.Threading.Thread.Sleep(20);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (p2[i])
                    {
                        //Э���ֽ�����
                        byte[] CloseCommand = new byte[5];
                        CloseCommand[0] = 0xFF;//��ʼ��
                        CloseCommand[1] = 0x00;//�豸��ַ�룬Ĭ��00
                        switch (i)
                        {
                            case 0:
                                CloseCommand[2] = 0x01;
                                break;
                            case 1:
                                CloseCommand[2] = 0x02;
                                break;
                            case 2:
                                CloseCommand[2] = 0x03;
                                break;
                            case 3:
                                CloseCommand[2] = 0x04;
                                break;
                            case 4:
                                CloseCommand[2] = 0x05;
                                break;
                            case 5:
                                CloseCommand[2] = 0x06;
                                break;
                            case 6:
                                CloseCommand[2] = 0x07;
                                break;
                            case 7:
                                CloseCommand[2] = 0x08;
                               break;
                        }
                        //���� �ر� ��־
                        if (playflag) CloseCommand[3] = 0x01;
                        else CloseCommand[3] = 0x00;
                        //������
                        CloseCommand[4] = 0x55;
                        portData.SendData(CloseCommand);
                        System.Threading.Thread.Sleep(20);
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
