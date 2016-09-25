﻿using Microsoft.AspNet.Identity;
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
            using (TransprtEntities entity = new TransprtEntities()) {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(DBContextIdentity));
                var rolAnonimo = roleManager.FindByName("Público");
                return entity.MenuByAreas.Where(menu => menu.id_area == rolAnonimo.Id).Select(menuByRol => menuByRol.Menu).ToList();
            }
        }

        public IEnumerable<Menu> GetMenuForCurrentUser(bool? isHome) {
            if(isHome.HasValue && isHome.Value) {
                return GetAllAnonimousMenus();
            }
            return GetUserMenu();
        }

        public IEnumerable<Menu> GetUserMenu() {
            using (TransprtEntities entity = new TransprtEntities()) {
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(DBContextIdentity));
                var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                if(user == null) {
                    return new List<Menu>();
                }
                var roles = user.Roles.Select(rol => rol.RoleId);
                return entity.MenuByAreas.Where(menu => roles.Contains(menu.id_area))
                            .Select(menuByRol => menuByRol.Menu).Distinct().ToList();
            }
        }

    }
}