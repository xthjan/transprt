using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Utils;

namespace Transprt.Controllers.Dashboard.Transportes {
    public class MarcasController : Controller {
        private TransprtEntities db = new TransprtEntities();

        // GET: Marcas
        public async Task<ActionResult> Index() {
            return View(await db.Marcas.ToListAsync());
        }


        // GET: Marcas/Create
        public ActionResult Create() {
            var marca = new Marca();
            marca.usr_crea = UtilAut.GetUserId();
            marca.fec_crea = DateTime.Now;
            marca.id = 0;
            return View(marca);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Marca marca) {
            if (ModelState.IsValid) {
                db.Marcas.Add(marca);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(marca);
        }


        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null) {
                return HttpNotFound();
            }
            return View("Create", marca);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Marca marca) {
            if (ModelState.IsValid) {
                marca.usr_modif = UtilAut.GetUserId();
                marca.fec_modif = DateTime.Now;
                db.Entry(marca).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Create", marca);
        }


        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null) {
                return HttpNotFound();
            }
            return View(marca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Marca marca = await db.Marcas.FindAsync(id);
            db.Marcas.Remove(marca);
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
