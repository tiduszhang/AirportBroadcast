using System.Collections.Generic;
using AirportBroadcast.Auditing.Dto;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
