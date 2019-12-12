using Abp.AutoMapper;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.Authorization.Users.Dto;
using AirportBroadcast.Web.Areas.Mpa.Models.Common;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}