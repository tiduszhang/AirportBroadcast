﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AirportBroadcast.Configuration.Tenants.Dto;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}