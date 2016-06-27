using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace MyEventCloud.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : MyEventCloudControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}