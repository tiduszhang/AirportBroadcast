using System.Collections.Generic;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using AirportBroadcast.Auditing.Dto;
using AirportBroadcast.DataExporting.Excel.EpPlus;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Auditing.Exporting
{
    public class AuditLogListExcelExporter : EpPlusExcelExporterBase, IAuditLogListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AuditLogListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos)
        {
            return CreateExcelPackage(
                "AuditLogs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AuditLogs"));
                    sheet.OutLineApplyStyle = true;

                    AddTitle(sheet, "审计日志", 1, 10);
                    AddQueryInfo(sheet, "日期：2019-01-13到2019-02-13；操作：GetCarryStations", 2, 10);

                    AddHeader(
                        sheet,
                        3,
                        L("Time"),
                        L("UserName"),
                        L("Service"),
                        L("Action"),
                        L("Parameters"),
                        L("Duration"),
                        L("IpAddress"),
                        L("Client"),
                        L("Browser"),
                        L("ErrorState")
                    );

                    AddObjects(
                        sheet,4, auditLogListDtos,
                        _ => _timeZoneConverter.Convert(_.ExecutionTime, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.UserName,
                        _ => _.ServiceName,
                        _ => _.MethodName,
                        _ => _.Parameters,
                        _ => _.ExecutionDuration,
                        _ => _.ClientIpAddress,
                        _ => _.ClientName,
                        _ => _.BrowserInfo,
                        _ => _.Exception.IsNullOrEmpty() ? L("Success") : _.Exception
                        );

                    //Formatting cells

                    var timeColumn = sheet.Column(1);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

                    for (var i = 1; i <= 10; i++)
                    {
                        if (i.IsIn(5, 10)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}