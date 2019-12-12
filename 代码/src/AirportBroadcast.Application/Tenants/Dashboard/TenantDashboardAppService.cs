using Abp.Authorization;
using AirportBroadcast.Authorization;

namespace AirportBroadcast.Tenants.Dashboard
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : AbpZeroTemplateAppServiceBase, ITenantDashboardAppService
    {
       
         

       
    }
}