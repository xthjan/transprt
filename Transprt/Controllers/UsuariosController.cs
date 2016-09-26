using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Transprt.Data.Identity;
using Transprt.Utils;

namespace Transprt.Controllers {
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller {
        private IdentityDBContext db = new IdentityDBContext();
        RoleManager<AppRole> RoleManager => new RoleManager<AppRole>(new RoleStore<AppRole>(db));
        UserManager<AppUser> UserManager => new UserManager<AppUser>(new UserStore<AppUser>(db));

        // GET: Usuarios
        public async Task<ActionResult> Index() {
            return View(await UserManager.Users.ToListAsync());
        }

        // GET: Usuarios/Create
        public ActionResult Create() {
            var appUser = new AppUser();
            AppUser.AsignaAreas(appUser);
            return View(appUser);
        }

        public bool InValidUser(AppUser appUser) {
            return string.IsNullOrWhiteSpace(appUser.FirstName) ||
                   string.IsNullOrWhiteSpace(appUser.LastName) ||
                   string.IsNullOrWhiteSpace(appUser.User) ||
                   (string.IsNullOrWhiteSpace(appUser.Id) && string.IsNullOrWhiteSpace(appUser.Password)) ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppUser appUser) {
            if (InValidUser(appUser)) {
                return View(appUser);
            }
            var user = UserManager.FindByName(appUser.User);
            if (user != null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El usuario ya existe");
                return View(appUser);
            }

            user = new AppUser() {
                UserName = appUser.User,
                Email = appUser.Email,
                EmailConfirmed = true,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                CreationDate = DateTime.Now,
                CreationUserName = UtilAut.GetUserId(),
                Password = appUser.Password,
                User = appUser.User,
                PhoneNumber = appUser.PhoneNumber,
                activo = appUser.activo
            };
            var result = await UserManager.CreateAsync(user, appUser.Password);
            var roles = appUser.RolesByUser.Where(role => role.Asignado).Select(role => role.Name).ToArray();
            if (roles.Length > 0) {
                UserManager.AddToRoles(user.Id, roles);
            }
            return RedirectToAction("Index");
        }

      
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = await UserManager.FindByIdAsync(id);
            if (appUser == null) {
                return HttpNotFound();
            }
            AppUser.AsignaAreasEdicion(appUser);
            appUser.User = appUser.UserName;
            return View(appUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( AppUser appUser) {
            if (InValidUser(appUser)) {
                return View(appUser);
            }
            var user = await UserManager.FindByIdAsync(appUser.Id);
            if (user == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El usuario no existe");
                return View(appUser);
            }
            user.UserName = appUser.User;
            user.Email = appUser.Email;
            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.Password = appUser.Password;
            user.User = appUser.User;
            user.PhoneNumber = appUser.PhoneNumber;
            user.activo = appUser.activo;
            if (!string.IsNullOrWhiteSpace(appUser.Password)) {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(appUser.Password);
            }
            user.Password = user.PasswordHash;
            var result = await UserManager.UpdateAsync(user);
            user.Roles.ToList().ForEach(rol => {
                var appRole = RoleManager.FindById(rol.RoleId);
                UserManager.RemoveFromRole(user.Id, appRole.Name);
            });
            var roles = appUser.RolesByUser.Where(role => role.Asignado).Select(role => role.Name).ToArray();
            if (roles.Length > 0) {
                UserManager.AddToRoles(user.Id, roles);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = await UserManager.FindByIdAsync(id);
            if (appUser == null) {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            AppUser appUser = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(appUser);
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
