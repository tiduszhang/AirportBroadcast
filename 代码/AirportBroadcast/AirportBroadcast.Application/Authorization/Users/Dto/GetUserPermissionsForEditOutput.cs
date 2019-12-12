using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AirportBroadcast.Authorization.Permissions.Dto;

namespace AirportBroadcast.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}