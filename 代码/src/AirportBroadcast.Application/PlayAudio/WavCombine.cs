using Abp.Dependency;
using AirportBroadcast.ActiveMQ;
using AirportBroadcast.AudioControl;
using AirportBroadcast.Domain.wavFileInfo;
using AirportBroadcast.PlayAudio.Dto;
using AutoMapper;
using Castle.Core.Logging;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;

namespace AirportBroadcast.PlayAudio
{
    public class WavCombine: IWavCombine, ISingletonDependency
    {
        private readonly ILogger logger;
        private static bool isPlaying = false;
        private static int next = 0;
        //更换了继电器，协议更换 波特率由1200改成9600
        PortData portData = new PortData(ConfigurationManager.AppSettings["COM"], 9600, Parity.None);
        bool[] p1 = new bool[8];//端口//声卡1

        int playerNum = 0; //声卡数量
        IWavePlayer[] wavePlayers;
        //声卡
        public IWavePlayer[] WavePlayers
        {
            get { return wavePlayers; }
        }
        private WaveStream reader;
        private readonly IAppFolders appFolders;
        public event Action<PlayStateData> PlayStateCallback;
        public List<PlayerAgs> playerAgslist;
        public BackgroundWorker _playBackgroundWorker;
        public WavCombine( 
            ILogger logger, IAppFolders appFolders)
        {
            this.logger = logger;
            this.appFolders = appFolders;
            playerAgslist = new List<PlayerAgs>();
            _playBackgroundWorker = new BackgroundWorker();
            _playBackgroundWorker.DoWork += _playBackgroundWorker_DoWork;
            _playBackgroundWorker.RunWorkerCompleted += _playBackgroundWorker_RunWorkerCompleted;

        }

        private void _playBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isPlaying = false;
        }

        private void _playBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            PlayAudio();
        }

        /// <summary>
        /// 清除播放队列 对外 
        /// </summary>
        /// <returns></returns>
        public bool ClearPlayQuenue()
        {
            if (playerAgslist != null || playerAgslist.Count()>1)
            {
                playerAgslist.Clear();
            }
            return true;
        }

        /// <summary>
        /// 生成wav文件  对外
        /// </summary>
        /// <param name="sourceFiles">源文件</param>
        /// <returns>返回生成之后的wav文件路径</returns>
        public WavFileData Concatenate(List<string> sourceFiles,string remark)
        {
            byte[] buffer = new byte[1024];
            int ret = 1;double length = 0;
            string root_path = Path.Combine(appFolders.AudioFolder, "Temp",DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(root_path))
            {
                Directory.CreateDirectory(root_path);
            }
            string wavName = (remark??"")+"_"+ Guid.NewGuid().ToString().Replace("-", "") + ".wav";
            string outputFile = Path.Combine(root_path, wavName); 
      
            WaveFileWriter waveFileWriter = null;
            try
            {
                //string firstBlank = "blank.wav";
                //sourceFiles.Insert(0, firstBlank);
                foreach (string sourceFile in sourceFiles)
                {
                    var fileName = Path.Combine(appFolders.AudioFolder, sourceFile);
                    if (File.Exists(fileName))
                    {
                        using (WaveFileReader reader = new WaveFileReader(fileName))
                        {
                            if (waveFileWriter == null)
                            {
                                // first time in create new Writer
                                waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                            }
                            else
                            {
                                if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                                {
                                    throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                                }
                            }
                            int read;
                            while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                waveFileWriter.Write(buffer, 0, read);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = 0;
                logger.Error(ex.Message, ex);
            }
            finally
            {
                if (waveFileWriter != null)
                {
                    length = waveFileWriter.TotalTime.TotalSeconds;
                    waveFileWriter.Dispose();
                }
            }
            var data = new WavFileDto
            {
                Ret = ret,
                FileName = wavName,
                WavLength = length
            };
            return Mapper.Map<WavFileData>(data);
        }

        /// <summary>
        /// 播放  对外
        /// </summary>
        /// <param name="fullFileName">播放文件路径</param>
        /// <param name="playerDevice">播放声卡</param>
        /// <param name="ports">播放端口 00011001 表示4.5.8端口打开 </param>
        /// <param name="priority">优先级 数值越大 优先级越高</param>
        public void Player(string fullFileName, string playerDevice, string ports, int priority =0)
        {
            try
            {
                if (!string.IsNullOrEmpty(fullFileName) && !string.IsNullOrEmpty(playerDevice) && !string.IsNullOrEmpty(playerDevice))
                {
                    logger.InfoFormat("播放文件Player:{0}:{1}", fullFileName, ports);

                    using (PlayerAgs player = new PlayerAgs())
                    {
                        player.FullFileName = fullFileName;
                        player.PlayerDevice = playerDevice;
                        player.Ports = ports;
                        player.Provider = priority;
                        player.Flag = true;
                        //播放通道A0或A1只是通道入口，最终还是由端口 1,2,3,4,5,6,7,8等播放

                        playerAgslist.Add(player);//此处只管往list集合中添加数据
                        playerAgslist.Sort((a, b) => a.Provider.CompareTo(b.Provider));
                        //第一次初始  要触发播放方法
                        if (!isPlaying)
                        {
                            //所需要的端口都未被占用  此时可以寻找声卡调用播放
                            //PlayAudio();
                            if (WavePlayers == null || WavePlayers.Count() < 1)
                            {
                                //获取声卡数量 然后初始化声卡，
                                CreatePlayer();
                            }
                            if (!_playBackgroundWorker.IsBusy)
                                _playBackgroundWorker.RunWorkerAsync();
                        }
                    }
                }
            }
            catch (Exception ex) {
                logger.ErrorFormat("播放文件异常:{0}" , ex.Message + ex.Source);
            }

        }

        /// <summary>
        /// 判断所在端口状态  是否可播放
        /// </summary>
        /// <param name="ports">00011001 表示4.5.8端口打开 </param>
        /// <returns></returns>
        public int PortState(string ports)
        {
            if (!string.IsNullOrEmpty(ports)) {
                for (int k = 0; k < ports.Length; k++) {
                    if (p1[k] == true && ports[k]=='1') {
                        return 6;
                    }
                }
            }
            else { return 6; }
            return 0;
        }

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="fullFileName">播放的语音文件地址</param>
        /// <param name="playerNum">播放通道  A0，A1等</param>
        /// <param name="ports">播放端口 1,2,3等</param>
        public void PlayAudio1()
        {
            isPlaying = true;
            logger.InfoFormat("播放文件start playerAgslist数量:{0}", playerAgslist.Count.ToString());
            int q = 0; int playcount = 0;
            while (playerAgslist.Count > 0 && playcount< playerAgslist.Count)
            {
                System.Threading.Thread.Sleep(500);
                if (next < playerNum)
                {
                    q++;
                    try
                    {
                        logger.InfoFormat("{1}_播放文件start内部 playerAgslist数量:{0}", playerAgslist.Count.ToString(),q.ToString());
                        playerAgslist.ForEach(playerAgs =>
                        {
                            //满足端口条件 开始寻找空闲声卡
                            for (int i = 0; i < playerNum; i++)
                            {
                                if (playerAgs.Flag)
                                {
                                    if (WavePlayers[i].PlaybackState == PlaybackState.Stopped && playerAgslist.Count > 0)
                                    {
                                        //此处判断播放中的端口是否满足全部可用，例如要打开端口1,3,6
                                        if (PortState(playerAgs.Ports) == 0)
                                        {
                                            try
                                            {
                                                ++next;
                                                logger.InfoFormat("{2}_正在播放文件Player:{0}:{1}", playerAgs.FullFileName, playerAgs.Ports,q.ToString());

                                                //设置端口 为占用
                                                setport(playerAgs.Ports, true);
                                                //设置继电器状态 开通
                                                SendCommand(playerAgs.Ports, true);
                                                reader = new MediaFoundationReader(Path.Combine(appFolders.AudioFolder, @"Temp\"+DateTime.Now.ToString("yyyy-MM-dd"), playerAgs.FullFileName));
                                                WavePlayers[i].Init(reader);
                                                PlayStateCallback?.Invoke(new PlayStateData
                                                {
                                                    FileName = playerAgs.FullFileName,
                                                    PlayState = 0
                                                });
                                                WavePlayers[i].Play(playerAgs);
                                                //从list中剔除
                                                //playerAgslist.Remove(playerAgs);
                                                playerAgs.Flag = false;
                                                playcount++;
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.ErrorFormat("循环播放队列异常:{0}" ,ex.Message + ex.Source);
                                                WavePlayerOnPlaybackStopped(WavePlayers[i], null);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        });
                    }
                    catch (Exception ex) {
                        logger.ErrorFormat("{1}_外围循环播放队列异常:{0}" , ex.Message + ex.Source,q.ToString());
                    }
                    logger.InfoFormat("{1}_播放文件 end playerAgslist数量:{0}", playerAgslist.Count.ToString(),q.ToString());

                    //playerAgslist.Clear();
                }
                if (playcount >= playerAgslist.Count) { playerAgslist.Clear(); }
            }
        }

        #region 新改造
        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="fullFileName">播放的语音文件地址</param>
        /// <param name="playerNum">播放通道  A0，A1等</param>
        /// <param name="ports">播放端口 1,2,3等</param>
        public void PlayAudio()
        {
            isPlaying = true;
            logger.InfoFormat("播放文件start playerAgslist数量:{0}", playerAgslist.Count.ToString());
            int q = 0; int playcount = 0;
            while (playerAgslist.Count > 0 && playcount < playerAgslist.Count)
            {
                System.Threading.Thread.Sleep(500);
                if (next < playerNum)
                {
                    q++;
                    try
                    {
                        logger.InfoFormat("{1}_播放文件start内部 playerAgslist数量:{0}", playerAgslist.Count.ToString(), q.ToString());
                        playerAgslist.ForEach(playerAgs =>
                        {
                        //满足端口条件 开始寻找空闲声卡
                        for (int i = 0; i < playerNum; i++)
                        {
                            if (playerAgs.Flag)
                            {
                                
                                if (WavePlayers[i].PlaybackState == PlaybackState.Stopped && playerAgslist.Count > 0 && wavePlayers[i].DeviceName == playerAgs.PlayerDevice)
                                {
                                    //此处判断播放中的端口是否满足全部可用，例如要打开端口1,3,6
                                    if (PortState(playerAgs.Ports) == 0)
                                    {
                                        try
                                        {
                                            ++next;
                                            logger.InfoFormat("{2}_正在播放文件Player:{0}:{1}", playerAgs.FullFileName, playerAgs.Ports, q.ToString());

                                            //设置端口 为占用
                                            setport(playerAgs.Ports, true);
                                            //设置继电器状态 开通
                                            SendCommand(playerAgs.Ports, true);
                                            logger.Info($"打开控制器端口:{playerAgs.Ports}");
                                            var msg = Path.Combine(appFolders.AudioFolder, "Temp", DateTime.Now.ToString("yyyy-MM-dd"), playerAgs.FullFileName);
                                                reader = new MediaFoundationReader(msg);
                                                WavePlayers[i].Init(reader);
                                                PlayStateCallback?.Invoke(new PlayStateData
                                                {
                                                    FileName = playerAgs.FullFileName,
                                                    PlayState = 0
                                                });

                                                WavePlayers[i].Play(playerAgs);
                                                //从list中剔除
                                                //playerAgslist.Remove(playerAgs);
                                                playerAgs.Flag = false;
                                                playcount++;
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.ErrorFormat("循环播放队列异常:{0}", ex.Message + ex.Source);
                                                WavePlayerOnPlaybackStopped(WavePlayers[i], null);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat("{1}_外围循环播放队列异常:{0}", ex.Message + ex.Source, q.ToString());
                    }
                    logger.InfoFormat("{1}_播放文件 end playerAgslist数量:{0}", playerAgslist.Count.ToString(), q.ToString());

                    //playerAgslist.Clear();
                }
                if (playcount >= playerAgslist.Count) { playerAgslist.Clear(); }
            }
        }
        #endregion

        private void setport(string ports, bool flag)
        {
            if (!string.IsNullOrEmpty(ports))
            {
                for (int k = 0; k < ports.Length; k++)
                {
                    if ( ports[k] == '1')
                    {
                        p1[k] = flag;
                    }
                }
            }
        }

        //playflag true 开 false 关
        #region 操作电源控制器  通讯协议
        private void SendCommand(string ports,bool playflag)
        {
            //打开端口
            if (!portData.IsOpen())
            {
                portData.Open();
            }
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i] =='1')
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
                    if (playflag) CloseCommand[3] = 0x01;
                    else CloseCommand[3] = 0x00;
                    //结束符
                    CloseCommand[4] = 0x55;

                    portData.SendData(CloseCommand);
                    //System.Threading.Thread.Sleep(20);
                }
            }
            
        }
        #endregion

        MMDevice[] mMDevices;
        #region 改造NAudio
        private void CreatePlayer()
        {
            //从后台设置过来的
            //string[] voice = { "High Definition Audio 设备", "2- USB Audio Device" };
            mMDevices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();

            //wavePlayers = new IWavePlayer[WaveOut.DeviceCount];
            wavePlayers = new IWavePlayer[mMDevices.Length];
            playerNum = WaveOut.DeviceCount;
            for (int deviceid = 0; deviceid < mMDevices.Length; deviceid++)
            {
               // var capabilities = WaveOut.GetCapabilities(deviceid);
                WavePlayers[deviceid] = new WaveOutEvent();
                WavePlayers[deviceid].PlaybackStopped += WavePlayerOnPlaybackStopped;
                wavePlayers[deviceid].DeviceNumber = deviceid;
                wavePlayers[deviceid].DeviceName = mMDevices[deviceid].FriendlyName;// deviceid.ToString().Trim();//capabilities.ProductName;
                //capabilities.ProductName;  //ProductName即是声卡名称
            }
        }
        /// <summary>
        /// 播放完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="stoppedEventArgs"></param>
        private void WavePlayerOnPlaybackStopped(object sender, StoppedEventArgs stoppedEventArgs)
        {
            //播放结束之后的事件    关闭端口占用
            var wavSender = sender as WaveOutEvent;
            if (wavSender.PlayerAgs!=null && !string.IsNullOrEmpty(wavSender.PlayerAgs.Ports)) {
                //设置端口 为占用
                setport(wavSender.PlayerAgs.Ports, false);
                //设置继电器状态
                SendCommand(wavSender.PlayerAgs.Ports, false);
                logger.Info($"关闭控制器端口:{wavSender.PlayerAgs.Ports}");
                try
                {
                    logger.InfoFormat("播放完成 Player:{0}:{1}", wavSender.PlayerAgs.FullFileName, wavSender.PlayerAgs.Ports);

                    PlayStateCallback?.Invoke(new PlayStateData
                    {
                        FileName = wavSender.PlayerAgs.FullFileName,
                        PlayState = 1
                    });
                }
                catch (Exception ex) {
                    //记录日志
                    logger.ErrorFormat("播放完成之后传给后台播放状态异常:{0}", ex.Message + ex.Source);
                }
            }
            isPlaying = false;
            --next;
        }

        #endregion
        /// <summary>
        /// 人工插播  其余队列全部清空
        /// </summary>
        public void Art_Player(string fullFileName, string playerDevice, string ports)
        {
           
            try
            {
                #region 存在正在播放的  强行关闭
                StopAllPlayer();
                #endregion
                Player(fullFileName, playerDevice, ports);
            }
            catch (Exception ex) {
                logger.ErrorFormat("人工插播异常:{0}", ex.Message + ex.Source);

            }
        }

        /// <summary>
        /// 强制关闭所有在播语音
        /// </summary>
        private void StopAllPlayer()
        {
            try
            {
                if (playerAgslist != null || playerAgslist.Count() > 1)
                {
                    playerAgslist.Clear();
                }
                //满足端口条件 开始寻找空闲声卡
                for (int i = 0; i < playerNum; i++)
                {
                    if (WavePlayers[i].PlaybackState == PlaybackState.Playing)
                    {
                        try
                        {
                            WavePlayers[i].Stop();
                        }
                        catch (Exception ex)
                        {
                            logger.ErrorFormat("结束播放队列异常:{0}", ex.Message + ex.Source);
                            WavePlayerOnPlaybackStopped(WavePlayers[i], null);
                        }
                        break;

                    }

                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("强制关闭所有在播语音异常:{0}", ex.Message + ex.Source);
            }
        }

        public void Art_PlayerTxt(string fullFileName, string playerDevice, string ports)
        {
            
            try
            {
                #region 存在正在播放的  强行关闭
                StopAllPlayer();
                #endregion
                setport(ports, true);
                //设置继电器状态 开通
                SendCommand(ports, true);

                //第一次初始  要触发播放方法
                if (!isPlaying)
                {
                    //所需要的端口都未被占用  此时可以寻找声卡调用播放
                    //PlayAudio();
                    if (WavePlayers == null || WavePlayers.Count() < 1)
                    {
                        //获取声卡数量 然后初始化声卡，
                        CreatePlayer();
                    }
                }

               
                for (int i = 0; i < WavePlayers.Length; i++)
                {
                    try
                    {


                        string desFileName =Path.Combine( Path.GetDirectoryName(fullFileName), Path.GetFileNameWithoutExtension(fullFileName) + "_"+i.ToString().Trim() + Path.GetExtension(fullFileName));
                        File.Copy(fullFileName, desFileName, true);
                        reader = new MediaFoundationReader(desFileName);
                        using (PlayerAgs player = new PlayerAgs())
                        {
                            player.FullFileName = desFileName;
                            player.PlayerDevice = WavePlayers[i].DeviceName;
                            player.Ports = ports;
                            player.Provider = 0;
                            player.Flag = true;
                            //播放通道A0或A1只是通道入口，最终还是由端口 1,2,3,4,5,6,7,8等播放   
                           
                            WavePlayers[i].Init(reader);
                            logger.InfoFormat("正在播放文件Player:{0}:{1}", desFileName, ports);

                            WavePlayers[i].Play(player);

                        }
                    }
                    catch (Exception ex) { WavePlayerOnPlaybackStopped(WavePlayers[i], null); }
                }
            }
            catch (Exception ex)
            {

                logger.ErrorFormat("人工插播异常:{0}", ex.Message + ex.Source);
            }
        }
        /// <summary>
        /// 获取可用声卡数据
        /// </summary>
        /// <returns></returns>
        public List<string> GetVoiceDevice()
        {
            var list = new List<string>();
            var mMDevices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToArray();
            foreach (var item in mMDevices) {
                list.Add(item.FriendlyName);
            }
            return list;
        }
    }
}
