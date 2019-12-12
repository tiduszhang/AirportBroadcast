using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Domain.activeMq
{
    public class ReceiveJson : Entity<long>
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

    }
}
