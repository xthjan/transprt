using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Utils;

namespace Transprt.Controllers {
    [Authorize]
    public class MenusController : Controller {
        private TransprtEntities db = new TransprtEntities();


        public async Task<ActionResult> Index() {
            return View(await db.Menus.ToListAsync());
        }


        public async Task<ActionResult> Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null) {
                return HttpNotFound();
            }
            return View(menu);
        }
        
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Menu menu) {
            if (string.IsNullOrEmpty(menu.name)
                || string.IsNullOrEmpty(menu.url)) {
                return View(menu);
            }
            menu.usr_crea = UtilAut.GetUserId();
            menu.fec_crea = DateTime.Now;
            db.Menus.Add(menu);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
            return View(menu);
        }


        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menus.FindAsync(id);
            if (menu == null) {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Menu menu) {
            if (string.IsNullOrEmpty(menu.name)
                || string.IsNullOrEmpty(menu.url)) {
                return View(menu);
            }
            menu.usr_modif = UtilAut.GetUserId();
            menu.fec_modif = DateTime.Now;
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
