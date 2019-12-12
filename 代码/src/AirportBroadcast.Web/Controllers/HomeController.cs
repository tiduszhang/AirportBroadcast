using Abp.Auditing;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AirportBroadcast.Web.Controllers
{
    public class HomeController : AbpZeroTemplateControllerBase
    {
        [DisableAuditing]
        public ActionResult Index()
        {
            //return RedirectToAction("Index", "SelfTkMachine");
            return View();
        }


    }
}