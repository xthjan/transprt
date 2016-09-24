using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Transprt.Data.Identity;
using System;

[assembly: OwinStartup(typeof(Transprt.Security.Startup))]
namespace Transprt.Security {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(IdentityDBContext.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieSecure = CookieSecureOption.SameAsRequest,
                ExpireTimeSpan=TimeSpan.FromHours(4),
                LogoutPath = new PathString("/Account/Logout"),
                CookieName="Transprt.oAuth"
            });
        }
    }
}
