﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    
    public class AirShowDataDto : EntityDto<long>
    {
        /// <summary>
        /// 航显接口类型
        /// </summary>
        public string Airshowtype { get; set; }

        /// <summary>
        /// 航班主键  96580
        /// </summary>
        public string Routeoid { get; set; }

        /// <summary>
        /// 共享航班号
        /// </summary>
        public string Shareflightno { get; set; }

        /// <summary>
        /// 航班日期
        /// </summary>
        public DateTime? FlightDateTime { get; set; }

        public string FlightDateTimeStr { get; set; }

        /// <summary>
        /// 航班执行日期
        /// </summary>
        public DateTime? ExecutionDateTime { get; set; }

        public string ExecutionDateTimeStr { get; set; }

        /// <summary>
        /// 航班号 JD5608
        /// </summary>
        public string FlightNo2 { get; set; }

        /// <summary>
        /// 三字码航班号 CBJ5608
        /// </summary>
        public string FlightNo3 { get; set; }

        /// <summary>
        /// 航司二字码 JD
        /// </summary>
        public string AirlineCode2 { get; set; }

        /// <summary>
        /// 航司三字码 CBJ
        /// </summary>
        public string AirlineCode3 { get; set; }

        /// <summary>
        /// 航班号（除航司部分） 5608
        /// </summary>
        public string FlightNum { get; set; }

        /// <summary>
        /// 航班性质 W/Z
        /// </summary>
        public string FlightMssion { get; set; }

        /// <summary>
        /// 航班类型 D
        /// </summary>
        public string FlightType { get; set; }

        /// <summary>
        /// 出港经停1三字码
        /// </summary>
        public string DepFIV1NO3 { get; set; }

        /// <summary>
        /// 出港经停1四字码
        /// </summary>
        public string DepFIV1NO4 { get; set; }

        /// <summary>
        /// 出港经停2三字码
        /// </summary>
        public string DepFIV2NO3 { get; set; }

        /// <summary>
        /// 出港经停2四字码
        /// </summary>
        public string DepFIV2NO4 { get; set; }

        /// <summary>
        /// 进港经停1三字码
        /// </summary>
        public string ArrFIV1NO3 { get; set; }
        /// <summary>
        /// 进港经停1四字码
        /// </summary>
        public string ArrFIV1NO4 { get; set; }

        /// <summary>
        /// 进港经停2三字码
        /// </summary>
        public string ArrFIV2NO3 { get; set; }

        /// <summary>
        /// 进港经停2四字码
        /// </summary>
        public string ArrFIV2NO4 { get; set; }

        /// <summary>
        /// 经停1三字码
        /// </summary>
        public string Fiv1No3 { get; set; }

        /// <summary>
        /// 经停1四字码
        /// </summary>
        public string Fiv1No4 { get; set; }

        /// <summary>
        /// 经停2三字码
        /// </summary>
        public string Fiv2No3 { get; set; }

        /// <summary>
        /// 经停2四字码
        /// </summary>
        public string Fiv2No4 { get; set; }

        /// <summary>
        /// 起场三字码 CKG
        /// </summary>
        public string ForgNo3 { get; set; }

        /// <summary>
        /// 起场四字码	ZUCK
        /// </summary>
        public string ForgNo4 { get; set; }

        /// <summary>
        /// 落场三字码	LJG
        /// </summary>
        public string FestNo3 { get; set; }

        /// <summary>
        /// 落场四字码	ZPLJ
        /// </summary>
        public string FestNo4 { get; set; }

        /// <summary>
        /// 计划起飞时间	2018-02-26T09:10:00 	1753-01-01T00:00:00
        ///表示该项时间没有值
        /// </summary>
        public DateTime? DepPlanTime { get; set; }

        public string DepPlanTimeStr { get; set; }

        /// <summary>
        /// 预计起飞时间	2018-02-26T09:12:00
        /// </summary>
        public DateTime? DepForecastTime { get; set; }

        public string DepForecastTimeStr { get; set; }

        /// <summary>
        /// 实际起飞时间	1753-01-01T00:00:00
        /// </summary>
        public DateTime? DepartTime { get; set; }

        public string DepartTimeStr { get; set; }

        /// <summary>
        /// 计划落地时间	2018-02-26T11:00:00
        /// </summary>
        public DateTime? ArrPlanTime { get; set; }

        public string ArrPlanTimeStr { get; set; }

        /// <summary>
        /// 预计落地时间	2018-02-26T11:00:00
        /// </summary>
        public DateTime? ArrForecastTime { get; set; }

        public string ArrForecastTimeStr { get; set; }

        /// <summary>
        /// 实际落地时间	1753-01-01T00:00:00(表示为空)
        /// </summary>
        public DateTime? ArriveTime { get; set; }

        public string ArriveTimeStr { get; set; }

        /// <summary>
        /// 航班状态	FPL	对应内容见下表
        /// </summary>
        public string FlightStatus { get; set; }

        /// <summary>
        /// 航班状态	 
        /// </summary>
        public string FlightStatus_Cn { get {
                var str = "";

                  switch (FlightStatus)
                {
                    case "NOR":
                        str = "正常";break;
                    case "DEP":
                        str = "起飞"; break;
                    case "CNL":
                        str = "取消"; break;
                    case "DLY":
                        str = "延误"; break;
                    case "ARR":
                        str = "到达"; break;
                    case "FAL":
                        str = "落地"; break;
                    case "BCK":
                        str = "备降"; break;
                    case "RLS":
                        str = "Release"; break;
                    case "RTN":
                        str = "返航"; break;            
                    default:
                        str = FlightStatus; break;
                };
                return str;
            } }

        /// <summary>
        /// 登机口	11
        /// </summary>
        public string Gate { get; set; }
        /// <summary>
        /// 登机口计划开启时间	2018-02-26T08:40:00
        /// </summary>
        public DateTime? Gateopentime { get; set; }

        public string GateopentimeStr { get; set; }

        /// <summary>
        /// 登机口计划关闭时间	2018-02-26T09:00:00
        /// </summary>
        public DateTime? Gateclosetime { get; set; }

        public string GateclosetimeStr { get; set; }
        /// <summary>
        /// 行李转盘	3
        /// </summary>
        public string Carousel { get; set; }
        /// <summary>
        /// 值机岛
        /// </summary>
        public string CheckinLoad { get; set; }
        /// <summary>
        /// 值机柜台
        /// </summary>
        public string CheckinCounter { get; set; }
        /// <summary>
        /// 值机开始时间
        /// </summary>
        public DateTime? CheckinTimeStart { get; set; }

        public string CheckinTimeStartStr { get; set; }
        /// <summary>
        /// 值机结束时间
        /// </summary>
        public DateTime? CheckinTimeEnd { get; set; }

        public string CheckinTimeEndStr { get; set; }
        /// <summary>
        /// 航班流转状态	SCHD
        /// </summary>
        public string FlightCirculationStatus { get; set; }

        public string FlightCirculationStatus_Cn
        {
            get
            {
                var str = "";

                switch (FlightCirculationStatus)
                {
                    case "EARR":
                        str = "预计到达"; break;
                    case "BOR":
                        str = "登机"; break;
                    case "ARR":
                        str = "到达"; break;
                    case "POK":
                        str = "关闭"; break;
                    case "TBR":
                        str = "过站登机"; break;
                    case "DEP":
                        str = "起飞"; break;
                    case "ONR":
                        str = "起飞"; break;
                    case "NST":
                        str = "到达"; break;
                    case "CKI":
                        str = "值机"; break;
                    case "CKOFF":
                        str = "值机截止"; break;
                    case "CKO":
                        str = "值机截止"; break;
                    case "URBOR":
                        str = "催促登机"; break;
                    case "LBD":
                        str = "催促登机"; break;
                    case "DLY":
                        str = "延误"; break;
                    case "CAN":
                        str = "取消"; break;
                    case "ALT":
                        str = "备降"; break;
                    case "RTN":
                        str = "返航"; break;
                    case "LBAG":
                        str = "末行李"; break;
                    case "FBAG":
                        str = "首行李"; break;
                    case "PEND":
                        str = "等待最新的状态"; break;
                    case "SCHD":
                        str = "计划"; break;
                    case "EDEP":
                        str = "预计起飞"; break;
                    case "ONSTAND":
                        str = "上轮档"; break;
                    case "OFFSTAND":
                        str = "下轮档"; break;
                    case "NOR":
                        str = "正常"; break;
                    case "READY":
                        str = "客齐"; break;                 
                    default:
                        str = FlightStatus; break;
                };
                return str;
            }
        }

        /// <summary>
        /// 异常原因
        /// </summary>
        public string FlightAbnormalReason { get; set; }
        /// <summary>
        /// 机号	B6415
        /// </summary>
        public string Freg { get; set; }
        /// <summary>
        /// 进出港标记	J
        /// </summary>
        public string DeporArrCode { get; set; }
        /// <summary>
        /// 本场代码	LJG
        /// </summary>
        public string LocalAirportCode { get; set; }
        /// <summary>
        /// 行李上架时间
        /// </summary>
        public DateTime? Xlsjtime { get; set; }

        public string XlsjtimeStr { get; set; }


        /// <summary>
        /// 行李下架时间
        /// </summary>
        public DateTime? Xlxjtime { get; set; }

        public string XlxjtimeStr { get; set; }

        /// <summary>
        /// 操作类型	MODIFY
        /// </summary>
        public string RouteType { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        public string Playtime { get; set; }
        /// <summary>
        /// 播放区域
        /// </summary>
        public string Playarea { get; set; }
        /// <summary>
        /// 播放类型
        /// </summary>
        public string Playtype { get; set; }
        /// <summary>
        /// 延误类型
        /// </summary>
        public string Dlytype { get; set; }
        /// <summary>
        /// 延误时间
        /// </summary>
        public string Dlytime { get; set; }

        public string DlytimeStr { get; set; }

        /// <summary>
        /// 柜台号码
        /// </summary>
        public string Counterno { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TableSchema { get; set; }
        /// <summary>
        /// 是否混合	False
        /// </summary>
        public string Ismixed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FooterSql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WhereSql { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// 客户端电脑名称
        /// </summary>
        public string ClientMachineName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GroupSql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrderSql { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// False
        /// </summary>
        public string IsPrintAll { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 老的登记口
        /// </summary>
        public string GateOld { get; set; }

    }
}
