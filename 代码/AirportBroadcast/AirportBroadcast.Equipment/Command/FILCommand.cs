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

        /// <summary>
        /// 读取航班信息
        /// </summary>
        public virtual void Read()
        {

        }

        /// <summary>
        /// 读取AP机场基础数据
        /// </summary>
        public virtual void ReadAP()
        {

        }

        /// <summary>
        /// 读取AL航空公司基础数据
        /// </summary>
        public virtual void ReadAL()
        {

        }

        /// <summary>
        /// 读取SV航班服务基础数据
        /// </summary>
        public virtual void ReadSV()
        {

        }

        /// <summary>
        /// 读取RM航班备注基础数据
        /// </summary>
        public virtual void ReadRM()
        {

        }

        /// <summary>
        /// 读取DL延误代码基础数据
        /// </summary>
        public virtual void ReadDL()
        {

        }

        /// <summary>
        /// 读取NA航班性质基础数据
        /// </summary>
        public virtual void ReadNA()
        {

        }
    }
}
