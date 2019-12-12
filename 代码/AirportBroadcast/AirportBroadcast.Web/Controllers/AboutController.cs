using System.Web.Mvc;

namespace AirportBroadcast.Web.Controllers
{
    public class AboutController : AbpZeroTemplateControllerBase
    {
        public ActionResult Index()
        {
           
            return View();
        }
	}
}