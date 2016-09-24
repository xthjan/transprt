using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transprt.Data;
using Transprt.Data.Identity;
using Transprt.Managers.Base;

namespace Transprt.Managers {
    public class MenuManager : BaseManager<MenuManager> {
        public MenuManager() {
        }

        public IEnumerable<Menu> GetAllAnonimousMenus() {
            using (TransportEntity entity = new TransportEntity()) {
                var context = new IdentityDBContext();
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
                var rolAnonimo = roleManager.FindByName("Anonimo");
                return entity.MenuByRoles.Where(menu => menu.id_role == rolAnonimo.Id).Select(menuByRol => menuByRol.Menu).ToList();
            }
        }

        public IEnumerable<Menu> GetMenuForCurrentUser(bool? isHome) {
            if(isHome.HasValue && isHome.Value) {
                return GetAllAnonimousMenus();
            }
            return GetUserMenu();
        }

        public IEnumerable<Menu> GetUserMenu() {
            using (TransportEntity entity = new TransportEntity()) {
                var context = new IdentityDBContext();
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
                var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                if(user == null) {
                    return new List<Menu>();
                }
                var roles = user.Roles.Select(rol => rol.RoleId);
                return entity.MenuByRoles.Where(menu => roles.Contains(menu.id_role))
                            .Select(menuByRol => menuByRol.Menu).Distinct().ToList();
            }
        }

    }
}