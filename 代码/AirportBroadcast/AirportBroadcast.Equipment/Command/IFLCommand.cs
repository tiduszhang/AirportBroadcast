using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 新增航班
    /// </summary>
    public class IFLCommand
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
        /// 航班信息
        /// </summary>
        public virtual FlightInfo FlightInfo { get; set; }

        /// <summary>
        /// 解析指令内容
        /// </summary>
        public virtual void Analysis()
        {
            var data = CommandString.Substring(1 + 5 + 3);//去除头部

            var fieldValues = data.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            FlightInfo flightInfo = new FlightInfo();
            var type = flightInfo.GetType();

            var i = 0;
            foreach (var fieldValue in fieldValues)
            {
                var temp = fieldValue.Split('=');
                var field = temp[0];//属性名称
                var value = temp[1];//属性对应的值
                var property = type.GetProperty(field);
                if (property != null)
                {
                    i++;
                    property.SetValue(flightInfo, value);
                }
            }

            this.FlightInfo = flightInfo;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public virtual void Save()
        {

        }


        /// <summary>
        /// 创建MQ消息体
        /// </summary>
        public virtual string CreateMQCommand()
        {
            if (FlightInfo == null)
            {
                return "";
            }

            AirShowData airShowData = new AirShowData();

            airShowData.RouteType = "MODIFY";

            airShowData.Routeoid = FlightInfo.URNO;//航班主键
            airShowData.Shareflightno = FlightInfo.JFNO;//共享航班号
            airShowData.FlightNo2 = FlightInfo.FLNO;//航班号

            airShowData.AirlineCode2 = FlightInfo.FLNO.Substring(0, 2);//航班号前半部分
            airShowData.FlightNum = FlightInfo.FLNO.Substring(2);//航班号后半部分

            airShowData.FlightMssion = FlightInfo.TTYP;//航班性质 
            //航班类型 I	国际 D	国内  Q	地区
            //航班区域属性（国际、国内、混合、地区，I表示国际航班、D表示国内航班、M表示混合航班、R表示港澳航班）
            airShowData.FlightType = FlightInfo.FLTI;//航班类型 

            airShowData.DepFIV1NO3 = FlightInfo.VIA3;//出港经停1三字码  进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码
            airShowData.DepFIV1NO4 = FlightInfo.VIA4;//出港经停1四字码  进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码

            airShowData.ArrFIV1NO3 = FlightInfo.VIA3;//进港经停1三字码 进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码
            airShowData.ArrFIV1NO4 = FlightInfo.VIA4;//进港经停1四字码 进港航班的最后一个经停机场/出港航班的第一个经停机场的三字代码

            airShowData.Fiv1No4 = FlightInfo.VIAL;//经停1四字码 顺序的航班经停机场列表（四字代码表示） FlightInfo.VIAN 经停机场个数
            airShowData.Fiv2No4 = FlightInfo.VIAL;//经停2四字码 顺序的航班经停机场列表（四字代码表示） FlightInfo.VIAN 经停机场个数


            airShowData.ForgNo3 = FlightInfo.ORG3;//起场三字码
            airShowData.ForgNo4 = FlightInfo.ORG4;//起场四字码

            airShowData.FestNo3 = FlightInfo.DES3;//起场三字码
            airShowData.FestNo4 = FlightInfo.DES4;//起场四字码

            if (!String.IsNullOrWhiteSpace(FlightInfo.STOD))
            {
                airShowData.DepPlanTime = Utils.ToDateTime(FlightInfo.STOD);//计划起飞时间
            }
            if (!String.IsNullOrWhiteSpace(FlightInfo.ETDU))
            {
                airShowData.DepForecastTime = Utils.ToDateTime(FlightInfo.ETDU);//预计起飞时间
            }
            if (!String.IsNullOrWhiteSpace(FlightInfo.AIRU))
            {
                airShowData.DepartTime = Utils.ToDateTime(FlightInfo.AIRU);//实际起飞时间
            }
            if (!String.IsNullOrWhiteSpace(FlightInfo.STOA))
            {
                airShowData.ArrPlanTime = Utils.ToDateTime(FlightInfo.STOA);//计划落地时间
            }
            if (!String.IsNullOrWhiteSpace(FlightInfo.ETAU))
            {
                airShowData.ArrForecastTime = Utils.ToDateTime(FlightInfo.ETAU);//预计落地时间
            }
            if (!String.IsNullOrWhiteSpace(FlightInfo.LNDU))
            {
                airShowData.ArriveTime = Utils.ToDateTime(FlightInfo.LNDU);//实际落地时间
            }

            airShowData.FlightStatus = FlightInfo.STAT;//航班状态

            airShowData.FlightCirculationStatus = FlightInfo.STAT; //航班流转状态   

            airShowData.Gate = FlightInfo.GAT1;//登机口 FlightInfo.GAT2

            airShowData.Carousel = FlightInfo.BLT1;//行李转盘FlightInfo.BLT2

            if (FlightInfo.AORD == "A")//（A表示进港，D表示出港）
            {
                airShowData.DeporArrCode = "J";//进出港标记 J/ C	进/出
            }
            else if (FlightInfo.AORD == "D")
            {
                airShowData.DeporArrCode = "C";//进出港标记  J/ C	进/出
            }

            airShowData.Dlytype = FlightInfo.DLCD;//延误类型
            airShowData.Dlytime = FlightInfo.DLTI;//延误时间

            return Newtonsoft.Json.JsonConvert.SerializeObject(airShowData);
        }
    }
}
