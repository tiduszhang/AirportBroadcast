using System.Collections.Generic;
using Abp.Application.Services.Dto;
using AirportBroadcast.Editions.Dto;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}