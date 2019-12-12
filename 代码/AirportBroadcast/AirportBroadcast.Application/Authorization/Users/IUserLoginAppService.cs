using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AirportBroadcast.Authorization.Users.Dto;

namespace AirportBroadcast.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
