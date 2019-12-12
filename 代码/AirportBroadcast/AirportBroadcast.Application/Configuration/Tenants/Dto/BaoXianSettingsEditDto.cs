using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Configuration.Tenants.Dto
{
    public class BaoXianSettingsEditDto
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string Pid { get; set; }

        [Required]
        public string Key { get; set; }
    }
}
