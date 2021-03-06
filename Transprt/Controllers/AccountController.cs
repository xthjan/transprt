﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Transprt.Data.Identity;
using Transprt.Utils;

namespace Transprt.Controllers {
    public class AccountController : Controller {
        public IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() {
            if (Request.IsAuthenticated) {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(AppUser usuario) {
            if (string.IsNullOrWhiteSpace(usuario.User) || string.IsNullOrWhiteSpace(usuario.Password)) {
                return View();
            }
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new IdentityDBContext()));
            var userLogged = userManager.Find(usuario.User, usuario.Password);
            if (userLogged == null) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "La contraseña o el usuario no son correctos, favor de intentarlo de nuevo");
                return View();
            }
            if (!userLogged.activo) {
                ModelState.AddModelError(UtilGral.ERROR_FROM_CONTROLLER, "El Usuario no se encuentra habilitado, favor de contactar al administrador");
                return View();
            }
            await SignInAsync(userLogged, usuario.RememberMe, userManager);
            return RedirectToAction("Index", "Dashboard");
        }
        private async Task SignInAsync(AppUser usuario, bool isPersistent, UserManager<AppUser> userManager) {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var claimsIdentity = await userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, claimsIdentity);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}