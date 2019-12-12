using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using AirportBroadcast.Web.Controllers;

namespace AirportBroadcast.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}