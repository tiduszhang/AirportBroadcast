using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Equipment
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 转换格式位yyyyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string value)
        {
            var year = Convert.ToInt32(value.Substring(0, 4));
            var month = Convert.ToInt32(value.Substring(4, 2));
            var day = Convert.ToInt32(value.Substring(4 + 2, 2));
            var hour = Convert.ToInt32(value.Substring(4 + 2 + 2, 2));
            var minute = Convert.ToInt32(value.Substring(4 + 2 + 2 + 2, 2));
            var second = Convert.ToInt32(value.Substring(4 + 2 + 2 + 2 + 2, 2));

            DateTime dateTime = new DateTime(year, month, day, hour, minute, second);

            return dateTime;
        }
    }
}
