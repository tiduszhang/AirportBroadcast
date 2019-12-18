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

        /// <summary>
        /// 转换成AirShowData对象
        /// </summary>
        /// <returns></returns>
        public AirShowData ToAirShowData()
        { 
            AirShowData airShowData = new AirShowData();

            airShowData.Routeoid = this.URNO;//航班主键
            airShowData.Shareflightno = this.JFNO;//共享航班号
            airShowData.FlightNo2 = this.FLNO;//航班号

            airShowData.AirlineCode2 = this.FLNO.Substring(0, 2);//航班号前半部分
            airShowData.FlightNum = this.FLNO.Substring(2);//航班号后半部分

            airShowData.DepFIV1NO3 = this.VIA3;//出港经停1三字码  进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码
            airShowData.DepFIV1NO4 = this.VIA4;//出港经停1四字码  进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码

            airShowData.ArrFIV1NO3 = this.VIA3;//进港经停1三字码 进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码
            airShowData.ArrFIV1NO4 = this.VIA4;//进港经停1四字码 进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码

            airShowData.Fiv1No4 = this.VIAL;//经停1四字码 顺序的航班经停机场列表（四字代码表示） this.VIAN 经停机场个数
            airShowData.Fiv2No4 = this.VIAL;//经停2四字码 顺序的航班经停机场列表（四字代码表示） this.VIAN 经停机场个数


            airShowData.ForgNo3 = this.ORG3;//起场三字码
            airShowData.ForgNo4 = this.ORG4;//起场四字码

            airShowData.FestNo3 = this.DES3;//起场三字码
            airShowData.FestNo4 = this.DES4;//起场四字码

            if (!String.IsNullOrWhiteSpace(this.STOD))
            {
                airShowData.DepPlanTime = Utils.ToDateTime(this.STOD);//计划起飞时间
            }
            if (!String.IsNullOrWhiteSpace(this.ETDU))
            {
                airShowData.DepForecastTime = Utils.ToDateTime(this.ETDU);//预计起飞时间
            }
            if (!String.IsNullOrWhiteSpace(this.AIRU))
            {
                airShowData.DepartTime = Utils.ToDateTime(this.AIRU);//实际起飞时间
            }
            if (!String.IsNullOrWhiteSpace(this.STOA))
            {
                airShowData.ArrPlanTime = Utils.ToDateTime(this.STOA);//计划落地时间
            }
            if (!String.IsNullOrWhiteSpace(this.ETAU))
            {
                airShowData.ArrForecastTime = Utils.ToDateTime(this.ETAU);//预计落地时间
            }
            if (!String.IsNullOrWhiteSpace(this.LNDU))
            {
                airShowData.ArriveTime = Utils.ToDateTime(this.LNDU);//实际落地时间
            }

            airShowData.LocalAirportCode = "LUM";//本地机场标识

            airShowData.FlightMssion = this.TTYP;//航班性质 
            //航班类型 I	国际 D	国内  Q	地区
            //航班区域属性（国际、国内、混合、地区，I表示国际航班、D表示国内航班、M表示混合航班、R表示港澳航班）
            airShowData.FlightType = this.FLTI;//航班类型 

            airShowData.FlightStatus = this.STAT;//航班状态  缺少键值对应
            airShowData.FlightCirculationStatus = this.STAT; //航班流转状态    缺少键值对应

            airShowData.Gate = !String.IsNullOrWhiteSpace(this.GAT1) ? this.GAT1 : this.GAT2;//登机口 this.GAT2
            airShowData.Carousel = !String.IsNullOrWhiteSpace(this.BLT1) ? this.BLT1 : this.BLT2;//行李转盘this.BLT2

            if (this.AORD == "A")//（A表示进港，D表示出港）
            {
                airShowData.DeporArrCode = "J";//进出港标记 J/ C	进/出
            }
            else if (this.AORD == "D")
            {
                airShowData.DeporArrCode = "C";//进出港标记  J/ C	进/出
            }

            airShowData.Dlytype = this.DLCD;//延误类型
            airShowData.Dlytime = this.DLTI;//延误时间
             
            return airShowData;
        }
    }
}
