using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
/*player 序列号
 *2010-01-25修改通讯协议
 *  xing
 */
namespace AirportBroadcast.PlayAudio
{

    public class Players
    {
        Audio[] audio;

        bool[] p1 = new bool[8];//端口//声卡1
        bool[] p2 = new bool[8];//端口//声卡2
        byte[] Command = new byte[13];//命令
        //更换了继电器，协议更换 波特率由1200改成9600
        PortData portData = new PortData("COM1", 9600, Parity.None);
        int[] Arr_PortDetail = new int[8];
        public Audio[] Audios
        {
            get { return audio; }
        }
        #region  //方法
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


        //playflag true 开 false 关
        #region 操作电源控制器  通讯协议
        private void SendCommand(bool playflag,  int PlayerNum)
       {
             //打开端口
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
                        //协议字节数组
                        byte[] CloseCommand = new byte[5];
                        CloseCommand[0] = 0xFF;//起始码
                        CloseCommand[1] = 0x00;//设备地址码，默认00
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
                        //0x01开通0x00关闭
                        if (playflag)   CloseCommand[3] = 0x01;
                        else CloseCommand[3] = 0x00;
                        //结束符
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
                        //协议字节数组
                        byte[] CloseCommand = new byte[5];
                        CloseCommand[0] = 0xFF;//起始码
                        CloseCommand[1] = 0x00;//设备地址码，默认00
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
                        //开启 关闭 标志
                        if (playflag) CloseCommand[3] = 0x01;
                        else CloseCommand[3] = 0x00;
                        //结束符
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
