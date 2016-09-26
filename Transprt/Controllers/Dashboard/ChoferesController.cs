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

namespace Transprt.Controllers.Dashboard {
    [Authorize]
    public class ChoferesController : Controller {
        private TransprtEntities db = new TransprtEntities();

        // GET: Choferes
        public async Task<ActionResult> Index() {
            var choferes = db.Choferes.Include(c => c.Persona);
            return View(await choferes.ToListAsync());
        }



        public ActionResult Create() {
            Chofere chofer = new Chofere();
            chofer.usr_crea = UtilAut.GetUserId();
            chofer.fec_crea = DateTime.Now;
            chofer.Persona = new Persona() {
                usr_crea = UtilAut.GetUserId(),
                fec_crea = DateTime.Now,
                activo = true
            };
            return View(chofer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Chofere chofere) {
            if (ModelState.IsValid) {
                db.Choferes.Add(chofere);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chofere);
        }

        // GET: Choferes/Edit/5
        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chofere chofere = await db.Choferes.FindAsync(id);
            if (chofere == null) {
                return HttpNotFound();
            }
            return View(chofere);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Chofere chofere) {
            if (ModelState.IsValid) {
                chofere.usr_modif = UtilAut.GetUserId();
                chofere.fec_modif = DateTime.Now;
                chofere.Persona.usr_modif = UtilAut.GetUserId();
                chofere.Persona.fec_modif = DateTime.Now;
                db.Entry(chofere.Persona).State = EntityState.Modified;
                db.Entry(chofere).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chofere);
        }

        // GET: Choferes/Delete/5
        public async Task<ActionResult> Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chofere chofere = await db.Choferes.FindAsync(id);
            if (chofere == null) {
                return HttpNotFound();
            }
            return View(chofere);
        }

        // POST: Choferes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id) {
            Chofere chofere = await db.Choferes.FindAsync(id);
            db.Personas.Remove(chofere.Persona);
            db.Choferes.Remove(chofere);
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
