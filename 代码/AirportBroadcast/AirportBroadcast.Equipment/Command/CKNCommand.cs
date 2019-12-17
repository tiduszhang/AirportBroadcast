using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 重新值机（重新办理乘机手续）------------- 暂不使用
    /// </summary>
    public class CKNCommand
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

        }
        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
