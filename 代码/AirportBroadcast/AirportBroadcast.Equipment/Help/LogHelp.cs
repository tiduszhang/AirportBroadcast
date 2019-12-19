using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class LogHelp
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private static log4net.ILog log = null;

        /// <summary>
        /// 写入日志 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="level"></param>
        public static void WriteToLog(string value, log4net.Core.Level level)
        {
            //log4net.Core.Level level = log4net.Core.Level.Error;
            if (log == null)
            {
                //string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + @"\config.log4net";

                string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "config.log4net"); ;
                if (File.Exists(path))
                {
                    log4net.GlobalContext.Properties["LogUrl"] = System.Environment.CurrentDirectory;// System.IO.Path.Combine(System.Environment.CurrentDirectory, "logs");
                    log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
                    log = log4net.LogManager.GetLogger("lognet");
                }
                else
                {
                    log = log4net.LogManager.GetLogger("");
                }
            }
            if (level == log4net.Core.Level.Debug)
            {
                log.Debug(value);
            }
            else if (level == log4net.Core.Level.Info)
            {
                log.Info(value);
            }
            else if (level == log4net.Core.Level.Error)
            {
                log.Error(value);
            }
            else if (level == log4net.Core.Level.Fatal)
            {
                log.Fatal(value);
            }
            else if (level == log4net.Core.Level.Warn)
            {
                log.Warn(value);
            }
            else
            {
                log.Info(value);
            }
            //try
            //{
            //    using (StreamWriter s = System.IO.File.AppendText(System.IO.Path.GetFullPath(Constant.ApplicationWorkPath + @"\Log.log")))
            //    {
            //        s.WriteLine(value);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}
        }

    }
}
