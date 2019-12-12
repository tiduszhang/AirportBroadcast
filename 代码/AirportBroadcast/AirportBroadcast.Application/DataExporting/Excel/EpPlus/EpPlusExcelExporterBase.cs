using System;
using System.Collections.Generic;
using System.IO;
using Abp.Collections.Extensions;
using Abp.Dependency;
using AirportBroadcast.DataExporting.Exporting;
using AirportBroadcast.Dto;
using AirportBroadcast.Net.MimeTypes;
using OfficeOpenXml;

namespace AirportBroadcast.DataExporting.Excel.EpPlus
{
    public abstract class EpPlusExcelExporterBase : AbpZeroTemplateServiceBase, ITransientDependency
    {
        public IAppFolders AppFolders { get; set; }

        protected FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        protected void AddTitle(ExcelWorksheet sheet, string title, int rowNum, int colLength)
        {
            var rang = sheet.Cells[rowNum, 1, rowNum, colLength];
            rang.Merge = true;
            rang.Value = title;
            rang.Style.Font.Bold = true;
            rang.Style.Font.Size = 20;
            rang.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            rang.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            rang.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
        }

        protected void AddQueryInfo(ExcelWorksheet sheet, string queryInfo, int rowNum, int colLength)
        {
            var rang = sheet.Cells[rowNum, 1, rowNum, colLength];
            rang.Merge = true;
            rang.Value = queryInfo;
            rang.Style.Font.Bold = false;
            rang.Style.Font.Size = 16;
            rang.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            rang.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            rang.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
        }


        protected void AddHeader(ExcelWorksheet sheet, int rowNum, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, rowNum, i + 1, headerTexts[i]);
            }
        }

        protected void AddHeader(ExcelWorksheet sheet, int rowNum, int columnIndex, string headerText)
        {
            var cell = sheet.Cells[rowNum, columnIndex];
            cell.Value = headerText;
            cell.Style.Font.Bold = true;
            cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
        }

        protected void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = sheet.Cells[i + startRowIndex, j + 1];
                    cell.Value = propertySelectors[j](items[i]);
                    cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                }
            }
        }


       



        protected void Save(ExcelPackage excelPackage, FileDto file)
        {
            var filePath = Path.Combine(AppFolders.TempFileDownloadFolder, file.FileToken);
            excelPackage.SaveAs(new FileInfo(filePath));
        }
    }
}