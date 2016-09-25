using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transprt.Data.Identity {
    sealed class IdentityConfiguration : DbMigrationsConfiguration<IdentityDBContext> {
        public IdentityConfiguration() {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(IdentityDBContext context) {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            AppUser adminUser = CreateOrGetAdminUser(userManager);
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
            CreateAndAssignAdminRole(userManager, roleManager, adminUser);
        }

        private void CreateAndAssignAdminRole(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppUser adminUser) {
            var roleName = "Administrador";
            var role = roleManager.FindByName(roleName);
            if (role == null) {
                var result = roleManager.Create(new AppRole {
                    Name = roleName,
                    CreationDate = DateTime.Now,
                    CreationUserName = adminUser.UserName
                });
                role = roleManager.FindByName(roleName);
            }
            if (adminUser.Roles == null || !adminUser.Roles.Any(userRole => userRole.RoleId == userRole.RoleId)) {
                userManager.AddToRoles(adminUser.Id, new string[] { roleName });
            }
        }

        private AppUser CreateOrGetAdminUser(UserManager<AppUser> userManager) {
            var userName = "admin";
            var user = userManager.FindByName(userName);
            if (user == null) {
                var password = "admin1234";
                user = new AppUser() {
                    UserName = userName,
                    Email = "xthjan@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Michel",
                    LastName = "Castillo",
                    CreationDate = DateTime.Now,
                    CreationUserName = userName,
                    Password = "qqqqq",
                    User="aaaaa"
                };
                var result = userManager.Create(user, password);
            }
            return user;
        }
    }
}
