using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transprt.Utils;

namespace Transprt.Data.Identity {
    public class IdentityDBContext : IdentityDbContext<AppUser> {


        public IdentityDBContext()
            : base(UtilAut.GetConnectionString(), throwIfV1Schema: false) {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IdentityDBContext, IdentityConfiguration>());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }


        public static IdentityDBContext Create() {
            return new IdentityDBContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            var discriminator = "DISC";
            modelBuilder.Entity<IdentityUserRole>().ToTable(UtilAut.GetTableUserRoles(), UtilAut.GetSchema())
                        .HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            modelBuilder.Entity<IdentityUserLogin>().ToTable(UtilAut.GetTableLogins(), UtilAut.GetSchema())
                        .HasKey(userLogin => new { userLogin.UserId, userLogin.ProviderKey, userLogin.LoginProvider });
            modelBuilder.Entity<IdentityUserClaim>().ToTable(UtilAut.GetTableClaims(), UtilAut.GetSchema());
            ConfigureEntityUsersTable(modelBuilder, discriminator);
            ConfigureEntitRolesTable(modelBuilder, discriminator);
        }

        private void ConfigureEntityUsersTable(DbModelBuilder modelBuilder, string discriminator) {
            var users = modelBuilder.Entity<IdentityUser>().ToTable(UtilAut.GetTableUsers(), UtilAut.GetSchema());
            users.Map<AppUser>(appUser => appUser.Requires(discriminator).HasValue("APPUSER"));
            users.HasMany(user => user.Roles).WithRequired().HasForeignKey(rUsers => rUsers.UserId);
            users.HasMany(user => user.Claims).WithRequired().HasForeignKey(uClaims => uClaims.UserId);
            users.HasMany(user => user.Logins).WithRequired().HasForeignKey(uLogins => uLogins.UserId);

        }
        private void ConfigureEntitRolesTable(DbModelBuilder modelBuilder, string discriminator) {
            var roles = modelBuilder.Entity<IdentityRole>().ToTable(UtilAut.GetTableRoles(), UtilAut.GetSchema());
            roles.Map<AppRole>(appRole => appRole.Requires(discriminator).HasValue("APPROLE"));
            roles.HasMany(role => role.Users).WithRequired().HasForeignKey(uRol => uRol.RoleId);
        }

        public override Task<int> SaveChangesAsync() {
            try {
                return base.SaveChangesAsync();
            } catch (DbEntityValidationException ex) {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); 
            } catch (Exception e) {
                throw e;
            }
        }        
    }
}
