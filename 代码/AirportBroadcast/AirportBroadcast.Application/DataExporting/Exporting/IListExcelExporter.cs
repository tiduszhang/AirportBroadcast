using AirportBroadcast.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportBroadcast.DataExporting.Exporting
{
    public interface IListExcelExporter
    {
        FileDto ExportToFile<T>(List<T> listData, string fileName,
            string[] colnames, int[] NoAutoFitColNum, string title = "", string queryInfo = "",
            params Func<T, object>[] propertySelectors);

        FileDto ExportToFile<T>(List<T> listData, string fileName,
                    string[] colnames, string title = "", string queryInfo = "",
                    params Func<T, object>[] propertySelectors);
    }
}
