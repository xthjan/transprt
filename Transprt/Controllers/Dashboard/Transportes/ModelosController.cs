using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Utils;

namespace Transprt.Controllers.Dashboard.Transportes {
    [Authorize]
    public class ModelosController : Controller {
        private TransprtEntities db = new TransprtEntities();

        public async Task<ActionResult> Index() {
            var modelos = db.Modelos.Include(m => m.Marca);
            return View(await modelos.ToListAsync());
        }


        public ActionResult Create() {
            Modelo modelo = new Modelo();
            modelo.usr_crea = UtilAut.GetUserId();
            modelo.fec_crea = DateTime.Now;
            GetMarcas(null);
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Modelo modelo) {
            if (ModelState.IsValid) {
                db.Modelos.Add(modelo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            GetMarcas(modelo);
            return View("Create", modelo);
        }

        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = await db.Modelos.FindAsync(id);
            if (modelo == null) {
                return HttpNotFound();
            }
            GetMarcas(modelo);
            return View("Create", modelo);
        }

        private void GetMarcas(Modelo modelo) {
            SelectList marcas = new SelectList(db.Marcas.Where(marca => marca.activo), "id", "nombre");
            if (modelo != null) {
                marcas = new SelectList(db.Marcas.Where(marca => marca.activo), "id", "nombre", modelo.id_marca);
            }
            ViewBag.id_marca = marcas;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Modelo modelo) {
            if (ModelState.IsValid) {
                modelo.usr_modif = UtilAut.GetUserId();
                modelo.fec_modif = DateTime.Now;
                db.Entry(modelo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            GetMarcas(modelo);
            return View(modelo);
        }

        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modelo modelo = await db.Modelos.FindAsync(id);
            if (modelo == null) {
                return HttpNotFound();
            }
            return View(modelo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Modelo modelo = await db.Modelos.FindAsync(id);
            db.Modelos.Remove(modelo);
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
