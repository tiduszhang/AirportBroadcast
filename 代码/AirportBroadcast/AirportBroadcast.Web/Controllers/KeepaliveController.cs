using Abp.Auditing;
using System.Web.Mvc;

namespace AirportBroadcast.Web.Controllers
{
    public class KeepaliveController : AbpZeroTemplateControllerBase
    {
        [DisableAuditing]
        public ActionResult Index()
        {
            return Content("i am aliving");
        }
    }
}