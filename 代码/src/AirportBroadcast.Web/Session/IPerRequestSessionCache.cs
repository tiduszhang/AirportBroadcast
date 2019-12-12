using System.Threading.Tasks;
using AirportBroadcast.Sessions.Dto;

namespace AirportBroadcast.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
