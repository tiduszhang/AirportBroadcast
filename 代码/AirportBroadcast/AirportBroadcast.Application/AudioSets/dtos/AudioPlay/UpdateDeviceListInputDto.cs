using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.AudioSets.dtos
{
    public class UpdateDeviceListInputDto
    {
        /// <summary>
        /// 1：国内/地方，2：国际
        /// </summary>
        public int DType { get; set; }

        public int Id { get; set; }

        public List<int> Dids { get; set; }
    }
}
