using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Transprt.Data.Identity;
using Transprt.Managers.Base;

namespace Transprt.Managers {
    public class UsuarioManager : BaseManager<UsuarioManager> {
        public string GetNombreCompletoById(string id) {
            var nombre = string.Empty;
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(DBContextIdentity));
            var user = userManager.FindById(id);
            if (user != null) {
                nombre = user.FirstName + " " + user.LastName;
            }
            return nombre;

        }
    }
}
