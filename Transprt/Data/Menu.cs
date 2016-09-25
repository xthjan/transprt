namespace Transprt.Data {
    using Identity;
    using Managers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    [Table("Menu")]
    public partial class Menu {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu() {
            MenuByAreas = new HashSet<MenuByArea>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string name { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Url")]
        public string url { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        [StringLength(128)]
        [Display(Name = "Creado Por")]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Creación")]
        public DateTime fec_crea { get; set; }

        [StringLength(128)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        [Display(Name = "Orden")]
        public int orden { get; set; }

        [Display(Name = "Áreas de acceso al menú")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuByArea> MenuByAreas { get; set; }

        public string GetUserCreador() {
            UsuarioManager usuarioManager = UsuarioManager.Instance;
            return usuarioManager.GetNombreCompletoById(usr_crea);
        }

        
        public static void UpdateMenus(Menu menu, IEnumerable<MenuByArea> asignedMenus) {
            if (asignedMenus == null) {
                return;
            }
            menu.MenuByAreas.ToList().ForEach(menuArea => {
                menu.MenuByAreas.Remove(menuArea);
            });
            asignedMenus.Where(menuArea => menuArea.Asignado).ToList().ForEach(menuArea => {
                menu.MenuByAreas.Add(new MenuByArea() { id_area = menuArea.id_area });
            });

        }
        public static void RemoveUnassignedArea(Menu menu) {
            menu.MenuByAreas.Where(menuArea => !menuArea.Asignado).ToList().ForEach(menuArea => {
                menu.MenuByAreas.Remove(menuArea);
            });
        }

        public static void AssignMenuToModel(Menu menu) {
            IdentityDBContext db = new IdentityDBContext();
            RoleManager<AppRole> RoleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(db));
            var activeRoles = RoleManager.Roles.Where(rol => rol.activo);
            var menusCreados = activeRoles.Select(rol => new MenuByArea() {
                id_area = rol.Id,
                Nombre = rol.Name
            }).ToList();
            if (menu.id != 0) {
                menusCreados = activeRoles.Select(rol => new MenuByArea() {
                    id_area = rol.Id,
                    Nombre = rol.Name,
                    id_menu = menu.id
                }).ToList();
                menusCreados.ForEach(menuAreaCreado => {
                    menuAreaCreado.Asignado = menu.MenuByAreas.Any(menuArea => menuArea.id_area == menuAreaCreado.id_area);
                });
            }
            menu.MenuByAreas = menusCreados;
        }
    }
}
