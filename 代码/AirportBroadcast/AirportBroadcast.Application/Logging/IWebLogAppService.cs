using Abp.Application.Services;
using AirportBroadcast.Dto;
using AirportBroadcast.Logging.Dto;

namespace AirportBroadcast.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
