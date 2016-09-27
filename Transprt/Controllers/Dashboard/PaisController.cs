using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data;
using Transprt.Utils;
using System.Linq;
using System.Collections.Generic;
using Transprt.Managers;

namespace Transprt.Controllers.Dashboard {
    [Authorize]
    public class PaisController : Controller {
        private TransprtEntities db = new TransprtEntities();


        public async Task<ActionResult> Index() {
            return View(await db.Paises.ToListAsync());
        }

        public ActionResult Create() {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pais pais) {
            if (ModelState.IsValid) {
                pais.fec_crea = DateTime.Now;
                pais.usr_crea = UtilAut.GetUserId();
                db.Paises.Add(pais);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pais);
        }

        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await db.Paises.FindAsync(id);
            if (pais == null) {
                return HttpNotFound();
            }
            return View(pais);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pais pais) {
            if (ModelState.IsValid) {
                pais.fec_modif = DateTime.Now;
                pais.usr_modif = UtilAut.GetUserId();
                db.Entry(pais).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pais);
        }


        public async Task<PartialViewResult> AddState(Estado estado) {
            if (Request.IsAjaxRequest()) {
                Pais pais = await db.Paises.FindAsync(estado.id_pais);
                pais.Estados.Add(new Estado() {
                    nombre = estado.nombre,
                    activo = true,
                    usr_crea = UtilAut.GetUserId(),
                    fec_crea = DateTime.Now
                });

                db.Entry(pais).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return PartialView("EstadoLista", pais.Estados);
            }
            return PartialView("_ErrorParcial");
        }

        public async Task<PartialViewResult> EditState(Estado estadoViewModel) {
            if (Request.IsAjaxRequest()) {
                Estado estado = db.Estados.FirstOrDefault(est => est.id == estadoViewModel.id);
                estado.nombre = estadoViewModel.nombre;
                db.Entry(estado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Pais pais = await db.Paises.FindAsync(estado.id_pais);
                return PartialView("EstadoLista", pais.Estados);
            }
            return PartialView("_ErrorParcial");
        }
        public async Task<PartialViewResult> EliminarEstado(int id) {
            if (Request.IsAjaxRequest()) {
                Estado estado = db.Estados.FirstOrDefault(est => est.id == id);
                var idPais = estado.id_pais;
                db.Entry(estado).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                Pais pais = await db.Paises.FindAsync(estado.id_pais);
                return PartialView("EstadoLista", pais.Estados);
            }
            return PartialView("_ErrorParcial");
        }

        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pais pais = await db.Paises.FindAsync(id);
            if (pais == null) {
                return HttpNotFound();
            }
            return View(pais);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Pais pais = await db.Paises.FindAsync(id);
            pais.Estados.ToList().ForEach(estado => {
                db.Estados.Remove(estado);
            });
            db.Paises.Remove(pais);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult GetEstados(int id) {
            var estados = PaisesManager.Instance.GetAllEstadosPorPais(id);
            return Json(estados, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
