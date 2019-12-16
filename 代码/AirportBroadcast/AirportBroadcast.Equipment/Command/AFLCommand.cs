using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 查询航班信息（包含航班基本信息与航班服务信息）
    /// </summary>
    public class AFLCommand
    {
        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }

        /// <summary>
        /// 开始时间yyyyMMddHHmmss
        /// </summary>
        public virtual string StartTime { get; set; }
        /// <summary>
        /// 开始时间yyyyMMddHHmmss
        /// </summary>
        public virtual string EndTime { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部
            if (data.Length == 14 + 14)
            {
                StartTime = data.Substring(0, 14);//开始时间长度14
                EndTime = data.Substring(14, 14);//结束时间长度14
            }
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StartTime = StartTime.PadRight(14, '0');
            EndTime = EndTime.PadRight(14, '0');
            return StartTime + EndTime;
        }
    }
}
