using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto; 
using AirportBroadcast.Auditing.Dto;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
         
    }
}