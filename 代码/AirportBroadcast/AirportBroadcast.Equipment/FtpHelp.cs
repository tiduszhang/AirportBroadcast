using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// FTP帮助类
    /// </summary>
    public class FtpHelp
    {
        /// <summary>
        /// FTP下载
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName"></param>
        public static void DownloadFile(string address, string fileName)
        {
            var configFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "tac.ini");
            if (!System.IO.File.Exists(configFile))
            {
                return;
            }

            var userName = IniFileHelp.Read("FTPSERVER", "UserName", "", configFile);
            var password = IniFileHelp.Read("FTPSERVER", "Password", "", configFile);
            var tempPath = IniFileHelp.Read("FTPSERVER", "TempPath", "", configFile);

            if (!System.IO.Directory.Exists(tempPath))
            {
                System.IO.Directory.CreateDirectory(tempPath);
            }

            var filePath = System.IO.Path.Combine(tempPath, fileName);
            using (WebClient webClient = new WebClient())
            {
                webClient.Credentials = new NetworkCredential(userName, password);
                webClient.DownloadFile(address, filePath);
            }
        }
    }
}
