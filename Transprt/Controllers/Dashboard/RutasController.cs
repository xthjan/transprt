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

namespace Transprt.Controllers.Dashboard
{
    public class RutasController : Controller
    {
        private TransprtEntities db = new TransprtEntities();

        // GET: Rutas
        public async Task<ActionResult> Index()
        {
            return View(await db.Rutas.ToListAsync());
        }


        // GET: Rutas/Create
        public ActionResult Create()
        {
            Ruta ruta = new Ruta() {
                usr_crea = UtilAut.GetUserId(),
                fec_crea = DateTime.Now,
                Direccione = new Direccione() {
                    usr_crea = UtilAut.GetUserId(),
                    fec_crea = DateTime.Now,
                    activo = true
                },
                Direccione1 = new Direccione() {
                    usr_crea = UtilAut.GetUserId(),
                    fec_crea = DateTime.Now,
                    activo = true
                }
            };
            return View("Create", ruta);
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ruta ruta)
        {
            ruta.Direccione.Estado = await db.Estados.FindAsync(ruta.Direccione.id_estado);
            ruta.Direccione1.Estado = await db.Estados.FindAsync(ruta.Direccione1.id_estado);

            if (TryValidateModel(ruta))
            {
                db.Rutas.Add(ruta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruta ruta = await db.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View("Create", ruta);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Ruta ruta)
        {
            ruta.Direccione.Estado = await db.Estados.FindAsync(ruta.Direccione.id_estado);
            ruta.Direccione1.Estado = await db.Estados.FindAsync(ruta.Direccione1.id_estado);
            ruta.fec_modif = DateTime.Now;
            ruta.usr_modif = UtilAut.GetUserId();
            ruta.Direccione.fec_modif = DateTime.Now;
            ruta.Direccione.usr_modif = UtilAut.GetUserId();
            ruta.Direccione1.fec_modif = DateTime.Now;
            ruta.Direccione1.usr_modif = UtilAut.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(ruta.Direccione1).State = EntityState.Modified;
                db.Entry(ruta.Direccione).State = EntityState.Modified;
                db.Entry(ruta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Create", ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruta ruta = await db.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return HttpNotFound();
            }
            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ruta ruta = await db.Rutas.FindAsync(id);
            db.Rutas.Remove(ruta);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
