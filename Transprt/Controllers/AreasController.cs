using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data.Identity;
using Transprt.Utils;

namespace Transprt.Controllers {
    [Authorize(Roles = "Administrador")]
    public class AreasController : Controller {
        private IdentityDBContext db = new IdentityDBContext();
        RoleManager<AppRole> RoleManager => new RoleManager<AppRole>(new RoleStore<AppRole>(db));

        public async Task<ActionResult> Index() {
            return View(await RoleManager.Roles.ToListAsync());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppRole appRole) {
            if (!ValidateModel(appRole, ModelState)) {
                return View(appRole);
            }
            var role = RoleManager.FindByName(appRole.Name);
            if (role != null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Área ya existe");
                return View(appRole);
            }
            var result = await RoleManager.CreateAsync(new AppRole {
                activo = appRole.activo,
                CreationDate = DateTime.Now,
                CreationUserName = UtilAut.GetUserId(),
                Name = appRole.Name
            });
            return RedirectToAction("Index");
        }

        public bool ValidateModel(AppRole appRole, ModelStateDictionary modelState) {
            if (string.IsNullOrWhiteSpace(appRole.Name)) {
                return false;
            }
            return true;
        }

        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRole appRole = await RoleManager.FindByIdAsync(id);
            if (appRole == null) {
                return HttpNotFound();
            }
            return View(appRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppRole appRole) {
            if (!ValidateModel(appRole, ModelState)) {
                return View(appRole);
            }
            var role = RoleManager.FindById(appRole.Id);
            if (role == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Área no existe");
                return View(appRole);
            }
            if (role.Id == Guid.Empty.ToString()) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "NOTA: El área pública no se puede deshabilitar");
                appRole.activo = true;
            }
            role.activo = appRole.activo;
            role.Name = appRole.Name;
            role.ModificationUserName = UtilAut.GetUserId();
            role.ModificationDate = DateTime.Now;
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRole appRole = await RoleManager.FindByIdAsync(id);
            if (appRole == null) {
                return HttpNotFound();
            }
            return View(appRole);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            var role = RoleManager.FindById(id);
            if (role == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Área no existe");
                return View(new AppRole());
            }
            if (role.Id == Guid.Empty.ToString()) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Área pública no puede eliminarse");
                return View(role);
            }
            await RoleManager.DeleteAsync(role);
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
