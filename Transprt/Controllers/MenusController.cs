using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Utils;
using System.Linq;

namespace Transprt.Controllers {
    [Authorize(Roles = "Administrador")]
    public class MenusController : Controller {
        private TransprtEntities db = new TransprtEntities();


        public async Task<ActionResult> Index() {
            return View(await db.Menus.ToListAsync());
        }
        
        public ActionResult Create() {
            Menu menu = new Menu();
            Menu.AssignMenuToModel(menu);
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Menu menuView) {
            if (string.IsNullOrEmpty(menuView.name)
                || string.IsNullOrEmpty(menuView.url)) {
                return View(menuView);
            }
            menuView.usr_crea = UtilAut.GetUserId();
            menuView.fec_crea = DateTime.Now;
            Menu.RemoveUnassignedArea(menuView);
            db.Menus.Add(menuView);            
            await db.SaveChangesAsync();            
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null) {
                return HttpNotFound();
            }
            Menu.AssignMenuToModel(menu);
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Menu menuView) {
            if (string.IsNullOrEmpty(menuView.name)
                || string.IsNullOrEmpty(menuView.url)) {
                return View(menuView);
            }
            Menu menu = await db.Menus.FindAsync(menuView.id);
            if (menu == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Menú no existe");
                return View(menuView);
            }
            menu.usr_modif = UtilAut.GetUserId();
            menu.fec_modif = DateTime.Now;
            Menu.UpdateMenus(menu, menuView.MenuByAreas);
            db.Entry(menu).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null) {
                return HttpNotFound();
            }
            return View(menu);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Menú no existe");
                return View();
            }
            menu.MenuByAreas.ToList().ForEach(menuArea => {
                db.MenuByAreas.Remove(menuArea);
            });
            db.Menus.Remove(menu);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
