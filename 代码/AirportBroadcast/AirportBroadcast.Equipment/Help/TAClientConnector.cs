using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航显设备中间件连接库
    /// </summary>
    public class TAClientConnector
    {
        public static int TA_SUCCESS = 1;
        public static int TA_TAIL = 0;
        public static int TA_QUEUE_EMPTY = -1;
        public static int TA_QUEUE_FULL = -2;
        public static int TA_LINKOK = 1;
        public static int TA_LINKERROR = 1;

        /// <summary>
        /// 连接 - 通过 tac.ini 文件获取配置
        /// </summary>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAStart();

        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="pcpDest"></param>
        /// <param name="pcpMsg"></param>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAPutMsg(string pcpDest, string pcpMsg);

        /// <summary>
        ///  发送消息
        /// </summary>
        /// <param name="pcpMsg"></param>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAPutMsgExt(string pcpMsg);

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="pcpDest"></param>
        /// <param name="pcpMsg"></param>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAGetMsg(StringBuilder pcpDest, StringBuilder pcpMsg);

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="pcpMsg"></param>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAGetMsgExt(StringBuilder pcpMsg);
         
        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAGetLinkStatus();

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        [DllImport("TAClient.dll")]
        public static extern int TAClose();
    }
}
