using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using AirportBroadcast.AudioControl;
using AirportBroadcast.Authorization;
using AirportBroadcast.Web.Controllers;

namespace AirportBroadcast.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : AbpZeroTemplateControllerBase
    {

        private readonly IWavCombine _wavCombine;

        public DashboardController( IWavCombine wavCombine)
        {
            _wavCombine = wavCombine;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}