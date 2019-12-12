using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AirportBroadcast.Domain.activeMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    [AutoMapFrom(typeof(ReceiveJson))]
    public class ReceiveJsonDto : EntityDto<long>
    {
        /// <summary>
        /// 具体内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 接受时间
        /// </summary>
        public DateTime ReciveTime { get; set; }

        public string Remark { get; set; }

        public string ReciveTimeStr { get { return ReciveTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
