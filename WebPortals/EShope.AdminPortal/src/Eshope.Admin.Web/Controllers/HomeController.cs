using Microsoft.AspNetCore.Mvc;

namespace Eshope.Admin.Web.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}