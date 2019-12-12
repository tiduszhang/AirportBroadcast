using System.Collections.Generic;
using AirportBroadcast.Authorization.Permissions.Dto;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}