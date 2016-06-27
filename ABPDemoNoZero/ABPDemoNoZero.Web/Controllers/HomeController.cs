using System.Web.Mvc;

namespace ABPDemoNoZero.Web.Controllers
{
    public class HomeController : ABPDemoNoZeroControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}