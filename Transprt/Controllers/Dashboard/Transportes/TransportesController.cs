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
    public class TransportesController : Controller {
        private TransprtEntities db = new TransprtEntities();


        public async Task<ActionResult> Index() {
            var transportes = db.Transportes.Include(t => t.TipoTransporte);
            return View(await transportes.ToListAsync());
        }


        public ActionResult Create() {
            var transporte = new Transporte();
            transporte.fec_crea = DateTime.Now;
            transporte.usr_crea = UtilAut.GetUserId();
            GetTipoTransporte(null);
            return View(transporte);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Transporte transporte) {
            if (ModelState.IsValid) {
                db.Transportes.Add(transporte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            GetTipoTransporte(transporte);
            return View(transporte);
        }

        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = await db.Transportes.FindAsync(id);
            if (transporte == null) {
                return HttpNotFound();
            }
            GetTipoTransporte(transporte);
            return View("Create",transporte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Transporte transporte) {
            if (ModelState.IsValid) {
                db.Entry(transporte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            GetTipoTransporte(transporte);
            return View("Create", transporte);
        }
        private void GetTipoTransporte(Transporte transporte) {
            SelectList tipTransp = new SelectList(db.TipoTransportes.Where(modelo => modelo.activo), "id", "descripcion");
            if (transporte != null) {
                tipTransp = new SelectList(db.TipoTransportes.Where(modelo => modelo.activo), "id", "descripcion", transporte.id_tipo_transporte);
            }
            ViewBag.id_tipo_transporte = tipTransp;
        }

        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = await db.Transportes.FindAsync(id);
            if (transporte == null) {
                return HttpNotFound();
            }
            return View(transporte);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Transporte transporte = await db.Transportes.FindAsync(id);
            db.Transportes.Remove(transporte);
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
