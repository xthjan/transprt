using System.Collections.Generic;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Managers;

namespace Transprt.Controllers {
    public class HomeController : Controller {
        MenuManager menuManager = MenuManager.Instance;
        const string OnHomeClass = "landing";
        public ActionResult Index() {
            ViewData.Add("MenuList", GetMenuAnonimo());
            ViewData.Add("OnHomeClass", OnHomeClass);
            return View();
        }

        public IEnumerable<Menu> GetMenuAnonimo() {
            return menuManager.GetAllAnonimousMenus();
        }
    }
}