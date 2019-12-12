using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using AirportBroadcast.Web.Areas.Mpa.Models.Common.Modals;
using AirportBroadcast.Web.Controllers;

namespace AirportBroadcast.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : AbpZeroTemplateControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}