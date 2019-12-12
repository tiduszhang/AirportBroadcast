using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Clients
{
    public class InsuranceResult<T>
    {
        public T result { get; set; }

        public bool success { get; set; }

        public Error error { get; set; }

        public class Error
        {
            public string message { get; set; }

            public string details { get; set; }
        }

    }

    public class InsuranceData
    {
        public string inputNo { get; set; }

        public string outputNo { get; set; }
        public string QRCode { get; set; }

        public string responseJson { get; set; }
        /// <summary>
        /// 意外伤残保额(输入)
        /// </summary>
        public string DClmamt { get; set; }
        /// <summary>
        /// 意外医疗保额
        /// </summary>
        public string DClmmedamt { get; set; }
    }
}
