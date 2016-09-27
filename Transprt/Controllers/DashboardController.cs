using System.Web.Mvc;

namespace Transprt.Controllers {

    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Transportes() {
            return View();
        }
    }
}