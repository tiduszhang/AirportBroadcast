using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using AirportBroadcast.Auditing.Dto;
using AirportBroadcast.DataExporting.Excel.EpPlus;
using AirportBroadcast.DataExporting.Exporting;
using AirportBroadcast.Dto;

namespace AirportBroadcast.Auditing.Exporting
{
    public class  ListExcelExporter : EpPlusExcelExporterBase, IListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile<T>(List<T> listData, string fileName,
            string[] colnames, int[] NoAutoFitColNum, string title = "", string queryInfo = "", 
            params Func<T, object>[] propertySelectors)
        {
            int rowNum = 1;        

            return CreateExcelPackage(
                fileName,
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AuditLogs"));
                    sheet.OutLineApplyStyle = true;
                    if (!string.IsNullOrEmpty(title))
                    {
                        AddTitle(sheet, title, rowNum, colnames.Length);
                        rowNum++;
                    }

                    if (!string.IsNullOrEmpty(queryInfo))
                    {
                        AddQueryInfo(sheet, queryInfo,rowNum, colnames.Length);
                        rowNum++;
                    }
                     
                    AddHeader(sheet, rowNum, colnames);
                    rowNum++;

                    AddObjects(
                        sheet, rowNum, listData,
                         propertySelectors
                        );

                   
                    for (var i = 1; i <= colnames.Length; i++)
                    {
                        if (i.IsIn(NoAutoFitColNum)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listData"></param>
        /// <param name="fileName"></param>
        /// <param name="colnames"></param>
        /// <param name="title"></param>
        /// <param name="queryInfo"></param>
        /// <param name="propertySelectors"></param>
        /// <returns></returns>
        public FileDto ExportToFile<T>(List<T> listData, string fileName,
            string[] colnames, string title = "", string queryInfo = "",
            params Func<T, object>[] propertySelectors)
        {
            return ExportToFile<T>(listData, fileName, colnames,new int[]{ }, title, queryInfo, propertySelectors);
        }
    }
}