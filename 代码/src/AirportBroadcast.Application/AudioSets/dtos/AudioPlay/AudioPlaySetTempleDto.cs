using AirportBroadcast.Baseinfo.dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.AudioSets.dtos
{
    public class AudioPlaySetTempleDto : AudioTemplteBaseDto
    {
        public int Tid { get; set; }

        public int Sort { get; set; }
    }

    public class AudioPlaySetTempleInputDto
    {
       
        public int Id { get; set; }

        /// <summary>
        /// 明细Id
        /// </summary>
        public int Tid { get; set; }

        /// <summary>
        /// 明细中的模版Id
        /// </summary>
        public int TempId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
