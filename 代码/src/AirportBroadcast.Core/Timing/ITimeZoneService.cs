using System.Threading.Tasks;
using Abp.Configuration;

namespace AirportBroadcast.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
