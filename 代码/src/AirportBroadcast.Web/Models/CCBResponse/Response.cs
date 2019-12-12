using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirportBroadcast.Web.Models.CCBResponse
{
    public class Response
    {
        public string SUCCESS { get; set; }
        public string PAYURL { get; set; }
        public string QRURL { get; set; }
    }
}