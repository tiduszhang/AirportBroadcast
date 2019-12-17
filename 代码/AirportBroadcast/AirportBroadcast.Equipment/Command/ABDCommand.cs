using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 查询基础数据（如机场代码基础数据，航空公司基础数据，服务代码基础数据等）
    /// </summary>
    public class ABDCommand
    {
        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }
         
        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
