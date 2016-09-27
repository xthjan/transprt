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
    public class TipoTransportesController : Controller {
        private TransprtEntities db = new TransprtEntities();

        public async Task<ActionResult> Index() {
            var tipoTransportes = db.TipoTransportes.Include(t => t.Modelo);
            return View(await tipoTransportes.ToListAsync());
        }

        public ActionResult Create() {
            TipoTransporte tipTrans = new TipoTransporte();
            tipTrans.usr_crea = UtilAut.GetUserId();
            tipTrans.fec_crea = DateTime.Now;
            tipTrans.id = 0;
            GetMarcas(null);
            return View(tipTrans);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TipoTransporte tipoTransporte) {
            if (ModelState.IsValid) {
                db.TipoTransportes.Add(tipoTransporte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            GetMarcas(tipoTransporte);
            return View(tipoTransporte);
        }

        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTransporte tipoTransporte = await db.TipoTransportes.FindAsync(id);
            if (tipoTransporte == null) {
                return HttpNotFound();
            }
            GetMarcas(tipoTransporte);
            return View("Create", tipoTransporte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TipoTransporte tipoTransporte) {
            if (ModelState.IsValid) {
                tipoTransporte.usr_modif = UtilAut.GetUserId();
                tipoTransporte.fec_modif = DateTime.Now;
                db.Entry(tipoTransporte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            GetMarcas(tipoTransporte);

            return View("Create", tipoTransporte);
        }

        private void GetMarcas(TipoTransporte tipTransp) {
            SelectList modelos = new SelectList(db.Modelos.Where(modelo => modelo.activo), "id", "nombre");
            if (tipTransp != null) {
                modelos = new SelectList(db.Modelos.Where(modelo => modelo.activo), "id", "nombre", tipTransp.id_modelo);
            }
            ViewBag.id_modelo = modelos;
        }

        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTransporte tipoTransporte = await db.TipoTransportes.FindAsync(id);
            if (tipoTransporte == null) {
                return HttpNotFound();
            }
            return View(tipoTransporte);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            TipoTransporte tipoTransporte = await db.TipoTransportes.FindAsync(id);
            db.TipoTransportes.Remove(tipoTransporte);
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
