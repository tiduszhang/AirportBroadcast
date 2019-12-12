using System.Threading.Tasks;
using Abp.Application.Services;
using AirportBroadcast.Configuration.Host.Dto;
using AirportBroadcast.Configuration.Tenants.Dto;

namespace AirportBroadcast.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();


        GeneralSettingsEditDto GetPlayWay();

        void SetPlayWay(GeneralSettingsEditDto input);

        GeneralSettingsEditDto GetPlayTimesAndCanPlayLanguages();
        void SetPlayTimesAndCanPlayLanguages(GeneralSettingsEditDto input);
    }
}
