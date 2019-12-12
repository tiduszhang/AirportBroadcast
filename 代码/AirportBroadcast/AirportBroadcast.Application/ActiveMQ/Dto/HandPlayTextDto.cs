using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.ActiveMQ.Dto
{
    public class HandPlayTextDto
    {
        public string PlayText { get; set; }

        public int PlayTimes { get; set; }

        public List<int> TopPortIds { get; set; }
    }
}
