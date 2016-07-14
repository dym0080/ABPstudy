using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace SNAS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SNASControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}