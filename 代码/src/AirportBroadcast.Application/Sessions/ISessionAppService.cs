using System.Threading.Tasks;
using Abp.Application.Services;
using AirportBroadcast.Sessions.Dto;

namespace AirportBroadcast.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
