using AirportBroadcast.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Baseinfo.dtos
{
   public class GetAllAudioInputDto: PagedAndSortedInputDto
    {
        public int? LanguageId { get; set; }

        public string FileName { get; set; }

        public string Code { get; set; }
    }
}
