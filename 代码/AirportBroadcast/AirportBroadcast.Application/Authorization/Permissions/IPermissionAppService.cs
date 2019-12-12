using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AirportBroadcast.Authorization.Permissions.Dto;

namespace AirportBroadcast.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
