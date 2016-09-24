using System.Web.Mvc;

namespace Transprt.Controllers {
    public class DashboardController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}