using System.Threading.Tasks;
using Abp.Application.Services;
using AirportBroadcast.Configuration.Host.Dto;

namespace AirportBroadcast.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

    

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
