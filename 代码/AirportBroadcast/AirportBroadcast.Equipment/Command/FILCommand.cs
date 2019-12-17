using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 返回查询结果所存的文件名(用于批量航班查询反馈)
    /// </summary>
    public class FILCommand
    {
        /// <summary>
        /// 指令等级
        /// </summary>
        public readonly string CommandLevel = "S";

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
