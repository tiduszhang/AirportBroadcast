using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Baseinfo.dtos
{
   public class AudioTemplteDetailInput
    {
        public int Id { get; set; }

        public AudioTemplteDetailDto Detail { get; set; }
    }

    public class AudioTemplteDetailDelInput
    {
        public int Id { get; set; }

        public int Did { get; set; }
    }
}
