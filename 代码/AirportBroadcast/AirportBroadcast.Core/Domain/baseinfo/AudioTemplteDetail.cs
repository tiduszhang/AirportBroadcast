using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportBroadcast.Domain.baseinfo
{
    public class AudioTemplteDetail : FullAuditedEntity
    {
        /// <summary>
        /// 是否为变量参数
        /// </summary>
        public bool IsParamter { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public ParamterType? ParamterType { get; set; }

        /// <summary>
        /// 常用语编号
        /// </summary>
        public virtual int? ConstId { get; set; }

        /// <summary>
        /// 常用语
        /// </summary>
        [ForeignKey("ConstId")]
        public virtual AudioConst AudioConst { get;set;}

        /// <summary>
        /// 模板编号
        /// </summary>
        public virtual int TemplteId { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        [ForeignKey("TemplteId")]
        public virtual AudioTemplte AudioTemplte { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int Sort { get; set; }
    }

    public enum ParamterType
    {
        Routeoid_航班主键 = 1,
        FlightDateTime_航班日期,
        ExecutionDateTime_航班执行日期,
        FlightNo2_航班号,
        FlightNo3_三字码航班号,
        AirlineCode2_航司二字码,
        AirlineCode3_航司三字码,
        FlightNum_航班号_除航司部分,
        FlightMssion_航班性质,
        FlightType_航班类型,
        DepFIV1NO3_出港经停1三字码,
        DepFIV1NO4_出港经停1四字码,
        DepFIV2NO3_出港经停2三字码,
        DepFIV2NO4_出港经停2四字码,
        ArrFIV1NO3_进港经停1三字码,
        ArrFIV1NO4_进港经停1四字码,
        ArrFIV2NO3_进港经停2三字码,
        ArrFIV2NO4_进港经停2四字码,
        Fiv1No3_经停1三字码,
        Fiv1No4_经停1四字码,
        Fiv2No3_经停2三字码,
        Fiv2No4_经停2四字码,
        ForgNo3_起场三字码,
        ForgNo4_起场四字码,
        FestNo3_落场三字码,
        FestNo4_落场四字码,
        DepPlanTime_计划起飞时间,
        DepForecastTime_预计起飞时间,
        DepartTime_实际起飞时间,
        ArrPlanTime_计划落地时间,
        ArrForecastTime_预计落地时间,
        ArriveTime_实际落地时间,
        FlightStatus_航班状态,
        Gate_登机口,
        Gateopentime_登机口计划开启时间,
        Gateclosetime_登机口计划关闭时间,
        Carousel_行李转盘,
        CheckinLoad_值机岛,
        CheckinCounter_值机柜台,
        CheckinTimeStart_值机开始时间,
        CheckinTimeEnd_值机结束时间,
        FlightCirculationStatus_航班流转状态,
        FlightAbnormalReason_异常原因,
        Freg_机号,
        DeporArrCode_进出港标记,
        LocalAirportCode_本场代码,
        Xlsjtime_行李上架时间,
        Xlxjtime_行李下架时间,
        Dlytype_延误类型,
        Dlytime_延误时间,
        Counterno_柜台号码
    }
}
