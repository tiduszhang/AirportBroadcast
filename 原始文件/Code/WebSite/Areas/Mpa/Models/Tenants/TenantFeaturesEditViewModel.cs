using Abp.AutoMapper;
using AirportBroadcast.MultiTenancy;
using AirportBroadcast.MultiTenancy.Dto;
using AirportBroadcast.Web.Areas.Mpa.Models.Common;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesForEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesForEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}