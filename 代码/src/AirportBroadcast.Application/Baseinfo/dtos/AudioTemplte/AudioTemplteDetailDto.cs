using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AirportBroadcast.Domain.baseinfo; 

namespace AirportBroadcast.Baseinfo.dtos
{
    /// <summary>
    /// 模板明细
    /// </summary>
    [AutoMap(typeof(AudioTemplteDetail))]
    public class AudioTemplteDetailDto : FullAuditedEntityDto
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
        public  int? ConstId { get; set; }

       /// <summary>
       /// 常用语
       /// </summary>
        public virtual AudioConstDto AudioConst { get;set;}

        /// <summary>
        /// 模板编号
        /// </summary>
        public int TemplteId { get; set; }
         
        /// <summary>
        /// 排列序号
        /// </summary>
        public int Sort { get; set; }
    }

     
}
