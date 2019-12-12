using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.Authorization.Users.Dto
{
    public class SetUserCarryStationInputDto
    {
        public long UserId { get; set; }

        public List<UserCarrystationSelectDto> AllCarrySets { get; set; }

    }
}
