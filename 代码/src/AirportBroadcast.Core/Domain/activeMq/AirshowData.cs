using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.activeMq
{
    public class AirshowData:Entity<long>
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
        public DateTime FlightDateTime { get; set; }

        /// <summary>
        /// 航班执行日期
        /// </summary>
        public DateTime ExecutionDateTime { get; set; }

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
        public DateTime DepPlanTime { get; set; }

        /// <summary>
        /// 预计起飞时间	2018-02-26T09:12:00
        /// </summary>
        public DateTime DepForecastTime { get; set; }

        /// <summary>
        /// 实际起飞时间	1753-01-01T00:00:00
        /// </summary>
        public DateTime DepartTime { get; set; }

        /// <summary>
        /// 计划落地时间	2018-02-26T11:00:00
        /// </summary>
        public DateTime ArrPlanTime { get; set; }

        /// <summary>
        /// 预计落地时间	2018-02-26T11:00:00
        /// </summary>
        public DateTime ArrForecastTime { get; set; }

        /// <summary>
        /// 实际落地时间	1753-01-01T00:00:00(表示为空)
        /// </summary>
        public DateTime ArriveTime { get; set; }

        /// <summary>
        /// 航班状态	FPL	对应内容见下表
        /// </summary>
        public string FlightStatus { get; set; }

        /// <summary>
        /// 登机口	11
        /// </summary>
        public string Gate { get; set; }
        /// <summary>
        /// 登机口计划开启时间	2018-02-26T08:40:00
        /// </summary>
        public DateTime Gateopentime { get; set; }
        /// <summary>
        /// 登机口计划关闭时间	2018-02-26T09:00:00
        /// </summary>
        public DateTime Gateclosetime { get; set; }
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
        public DateTime CheckinTimeStart { get; set; }
        /// <summary>
        /// 值机结束时间
        /// </summary>
        public DateTime CheckinTimeEnd { get; set; }
        /// <summary>
        /// 航班流转状态	SCHD
        /// </summary>
        public string FlightCirculationStatus { get; set; }
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
        public DateTime Xlsjtime { get; set; }
        /// <summary>
        /// 行李下架时间
        /// </summary>
        public DateTime Xlxjtime { get; set; }
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
        public DateTime Dlytime { get; set; }
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

        public virtual long Rid { get; set; }


        public DateTime ReciveTime { get; set; }

        [ForeignKey("Rid")]
        public ReceiveJson ReceiveJson { get; set; }

        public virtual List<CommAudioFileName> FileNames { get; set; }

}
}
