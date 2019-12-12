using System.Collections.Generic;
using AirportBroadcast.Authorization.Users.Dto;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}