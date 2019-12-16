using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 指令类型
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 查询航班信息（包含航班基本信息与航班服务信息）
        /// </summary>
        AFL,
        /// <summary>
        /// 查询基础数据（如机场代码基础数据，航空公司基础数据，服务代码基础数据等）
        /// </summary>
        ABD,
        /// <summary>
        /// 返回查询结果所存的文件名(用于批量航班查询反馈)
        /// </summary>
        FIL,
        /// <summary>
        /// 通知取文件(航班计划文件)
        /// </summary>
        NTI,
        /// <summary>
        /// 更新航班
        /// </summary>
        UFL,
        /// <summary>
        /// 新增航班
        /// </summary>
        IFL,
        /// <summary>
        /// 删除航班
        /// </summary>
        DFL,
        /// <summary>
        /// 更新航班服务
        /// </summary>
        USR,
        /// <summary>
        /// 新增航班服务
        /// </summary>
        ISR,
        /// <summary>
        /// 删除航班服务
        /// </summary>
        DSR,
        /// <summary>
        /// 催促登机
        /// </summary>
        URG,
        /// <summary>
        /// 重新值机（重新办理乘机手续）
        /// </summary>
        CKN
    }
}
