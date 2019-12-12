using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using AirportBroadcast.Authorization;
using AirportBroadcast.Web.Controllers;

namespace AirportBroadcast.Web.Areas.Mpa.Controllers
{
    [DisableAuditing]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_AuditLogs)]
    public class AuditLogsController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}