using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 查询基础数据（如机场代码基础数据，航空公司基础数据，服务代码基础数据等）
    /// 例子中AP表示机场，AL表示航空公司，SV表示服务）
    /// </summary>
    public class ABDCommand
    {
        /// <summary>
        /// 指令等级
        /// </summary>
        public readonly string CommandLevel = "U";

        /// <summary>
        /// 子类型AP表示机场，AL表示航空公司，SV表示服务）
        /// </summary>
        public static readonly string AP = "AP";
        /// <summary>
        /// 子类型AP表示机场，AL表示航空公司，SV表示服务）
        /// </summary>
        public static readonly string AL = "AL";
        /// <summary>
        /// 子类型AP表示机场，AL表示航空公司，SV表示服务）
        /// </summary>
        public static readonly string SV = "SV";

        /// <summary>
        /// 指令字符串
        /// </summary>
        public virtual string CommandString { get; set; }

        /// <summary>
        /// 指令子类型AP表示机场，AL表示航空公司，SV表示服务）
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("#{0}#", Type);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }
    }
}
