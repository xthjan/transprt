namespace Transprt.Data
{
    using Managers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Menu()
        {
            MenuByAreas = new HashSet<MenuByArea>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name ="Nombre")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuByArea> MenuByAreas { get; set; }
       
        public string GetUserCreador() {
            UsuarioManager usuarioManager = UsuarioManager.Instance;
            return usuarioManager.GetNombreCompletoById(usr_crea);
        }

    }
}
