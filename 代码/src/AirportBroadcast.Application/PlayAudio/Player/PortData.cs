using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
/*com�ڵ���*/
namespace AirportBroadcast.PlayAudio
{
    public class PortData
    {
        public event PortDataReceivedEventHandle Received;
        public event SerialErrorReceivedEventHandler Error;
        public SerialPort port;
         
        public bool ReceiveEventFlag = false;  //�����¼��Ƿ���Ч false��ʾ��Ч

        public PortData(string sPortName, int baudrate, Parity parity )
        {
            port = new SerialPort(sPortName, baudrate, parity, 8, StopBits.One);
            port.RtsEnable = true;
            port.ReadTimeout = 3000;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            port.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorEvent);
        }

        ~PortData()
        {
            Close();
        }
        public void Open()
        {
            if (!port.IsOpen)
            {
                try
                {
                    port.Open();
                }
                catch (Exception ex) {
                    var a = ex;
                }
            }
        }

        public void Close()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
        //���ݷ���
        public void SendData(byte[] data)
        {
            if (port.IsOpen)
            {
                port.Write(data, 0, data.Length);
            }
        }
        public void SendData(byte[] data, int offset, int count)
        {
            if (port.IsOpen)
            {
                port.Write(data, offset, count);
            }
        }
        //��������
        public int SendCommand(byte[] SendData, ref  byte[] ReceiveData, int Overtime)
        {

            if (port.IsOpen)
            {
                ReceiveEventFlag = true;        //�رս����¼�
                port.DiscardInBuffer();         //��ս��ջ�����                 
                port.Write(SendData, 0, SendData.Length);
                int num = 0, ret = 0;
                while (num++ < Overtime)
                {
                    if (port.BytesToRead >= ReceiveData.Length) break;
                    System.Threading.Thread.Sleep(1);
                }
                if (port.BytesToRead >= ReceiveData.Length)
                    ret = port.Read(ReceiveData, 0, ReceiveData.Length);
                ReceiveEventFlag = false;       //���¼�
                return ret;
            }
            return -1;
        }

        public void ErrorEvent(object sender, SerialErrorReceivedEventArgs e)
        {
            if (Error != null) Error(sender, e);
        }
        //���ݽ���
        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //��ֹ�����¼�ʱֱ���˳�
            if (ReceiveEventFlag) return;

            byte[] data = new byte[port.BytesToRead];
            port.Read(data, 0, data.Length);
            if (Received != null) Received(sender, new PortDataReciveEventArgs(data));
        }

        public bool IsOpen()
        {
            return port.IsOpen;
        }
    }
    public delegate void PortDataReceivedEventHandle(object sender, PortDataReciveEventArgs e);
    public class PortDataReciveEventArgs : EventArgs
    {
        public PortDataReciveEventArgs()
        {
            this.data = null;
        }

        public PortDataReciveEventArgs(byte[] data)
        {
            this.data = data;
        }

        private byte[] data;

        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
