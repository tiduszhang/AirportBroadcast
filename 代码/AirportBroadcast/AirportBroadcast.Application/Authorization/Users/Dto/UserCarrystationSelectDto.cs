using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Authorization.Users.Dto
{
    public class UserCarrystationSelectDto
    {
        public bool IsSelected { get; set; }

        public int Cid { get; set; }

        public string Cname { get; set; }
    }
}
