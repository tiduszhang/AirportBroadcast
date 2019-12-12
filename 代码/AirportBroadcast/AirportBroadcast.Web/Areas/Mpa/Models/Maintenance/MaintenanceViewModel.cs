using System.Collections.Generic;
using AirportBroadcast.Caching.Dto;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}