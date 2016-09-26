using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Transprt.Managers;
using System.Linq;

namespace Transprt.Data.Identity {

    public class AppUser : IdentityUser {

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreationDate { get; set; }

        [StringLength(128)]
        [Required]
        [Display(Name = "Creado Por")]
        public string CreationUserName { get; set; }

        [Display(AutoGenerateField = false)]
        public DateTime? ModificationDate { get; set; }

        [Display(AutoGenerateField = false)]
        [StringLength(128)]
        public string ModificationUserName { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType) {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
        public string GetUserCreador() {
            UsuarioManager usuarioManager = UsuarioManager.Instance;
            return usuarioManager.GetNombreCompletoById(CreationUserName);
        }

        [NotMapped]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida")]
        [NotMapped]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "El usuario es requerido")]
        [NotMapped]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [NotMapped]
        public List<AppRole> RolesByUser { get; set; }
        public static void AsignaAreas(AppUser usuario) {
            IdentityDBContext db = new IdentityDBContext();
            RoleManager<AppRole> roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(db));
            usuario.RolesByUser = roleManager.Roles.Where(role => role.Id != Guid.Empty.ToString() && role.activo).ToList();          
        }

        public static void AsignaAreasEdicion(AppUser usuario) {
            AsignaAreas(usuario);
            usuario.RolesByUser.ForEach(rol => {
                rol.Asignado = usuario.Roles.Any(rolUsuario => rolUsuario.RoleId == rol.Id);
            });
        }

    }
}
