using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 航班信息
    /// </summary>
    public class FlightInfo
    {
        #region 对应于航班信息的字段以及适用命令

        /// <summary>
        /// 唯一记录号
        /// </summary>
        public string URNO { get; set; }

        /// <summary>
        /// 航班号（航空公司、号码、后缀）
        /// </summary>
        public string FLNO { get; set; }

        /// <summary>
        /// 代码共享航班号（最多10个）
        /// </summary>
        public string JFNO { get; set; }

        /// <summary>
        /// 代码共享航班数量
        /// </summary>
        public string JCNT { get; set; }

        /// <summary>
        /// 航班状态
        /// </summary>
        public string STAT { get; set; }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string REGN { get; set; }

        /// <summary>
        /// 机型五码
        /// </summary>
        public string ACT5 { get; set; }

        /// <summary>
        /// 航班进出港标志（A表示进港，D表示出港）
        /// </summary>
        public string AORD { get; set; }

        /// <summary>
        /// 航班区域属性（国际、国内、混合、地区，I表示国际航班、D表示国内航班、M表示混合航班、R表示港澳航班）
        /// </summary>
        public string FLTI { get; set; }

        /// <summary>
        /// 航班性质（如W/Z代表正班）
        /// </summary>
        public string TTYP { get; set; }

        /// <summary>
        /// 目的机场三字代码
        /// </summary>
        public string DES3 { get; set; }

        /// <summary>
        /// 目的机场四字代码
        /// </summary>
        public string DES4 { get; set; }

        /// <summary>
        /// 进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码
        /// </summary>
        public string VIA3 { get; set; }

        /// <summary>
        /// 进港航班的最后一个经停机场/出港航班的第一个经停机场的四字代码
        /// </summary>
        public string VIA4 { get; set; }

        /// <summary>
        /// 顺序的航班经停机场列表（四字代码表示）
        /// </summary>
        public string VIAL { get; set; }

        /// <summary>
        /// 经停机场个数
        /// </summary>
        public string VIAN { get; set; }

        /// <summary>
        /// 机位
        /// </summary>
        public string PSTN { get; set; }

        /// <summary>
        /// 计划离港时间
        /// </summary>
        public string STOD { get; set; }

        /// <summary>
        /// 预计离港时间
        /// </summary>
        public string ETDU { get; set; }

        /// <summary>
        /// 实际起飞时间
        /// </summary>
        public string AIRU { get; set; }

        /// <summary>
        /// 登机门1
        /// </summary>
        public string GAT1 { get; set; }

        /// <summary>
        /// 登机门2
        /// </summary>
        public string GAT2 { get; set; }

        /// <summary>
        /// 始发机场三字代码
        /// </summary>
        public string ORG3 { get; set; }

        /// <summary>
        /// 始发机场四字代码
        /// </summary>
        public string ORG4 { get; set; }

        /// <summary>
        /// 计划到达时间
        /// </summary>
        public string STOA { get; set; }

        /// <summary>
        /// 预计到达时间
        /// </summary>
        public string ETAU { get; set; }

        /// <summary>
        /// 落地时间
        /// </summary>
        public string LNDU { get; set; }

        /// <summary>
        /// 上轮档时间
        /// </summary>
        public string ONBU { get; set; }

        /// <summary>
        /// 撤轮档时间
        /// </summary>
        public string OFBU { get; set; }

        /// <summary>
        /// 行李提取转盘1
        /// </summary>
        public string BLT1 { get; set; }

        /// <summary>
        /// 行李提取转盘2
        /// </summary>
        public string BLT2 { get; set; }

        /// <summary>
        /// 延误代码
        /// </summary>
        public string DLCD { get; set; }

        /// <summary>
        /// 延误时间（单位：分钟）
        /// </summary>
        public string DLTI { get; set; }

        /// <summary>
        /// S表示航班是计划，O表示执行状态
        /// </summary>
        public string FTYP { get; set; }

        /// <summary>
        /// 航显航班备注代码
        /// </summary>
        public string REMC { get; set; }

        /// <summary>
        /// 连班号
        /// </summary>
        public string RKEY { get; set; }

        /// <summary>
        /// 备降信息
        /// </summary>
        public string DIVR { get; set; }

        #endregion
       
        #region NA航班性质基础数据

        ///// <summary>
        ///// 航班性质代码
        ///// </summary>
        //public string TTYP { get; set; }

        /// <summary>
        /// 航班性质名称
        /// </summary>
        public string TNAMME { get; set; }


        #endregion
         
    }
}
