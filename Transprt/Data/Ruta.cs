namespace Transprt.Data {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ruta {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ruta() {
            AsignacionRutas = new HashSet<AsignacionRuta>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Ruta")]
        public string nombre { get; set; }

        [Display(Name = "Dirección Inicio")]
        public int id_direccion_inicio { get; set; }

        [Display(Name = "Dirección Final")]
        public int id_dirección_final { get; set; }

        [Display(Name = "KM Total")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal km_total { get; set; }

        [Display(Name = "Gasto Gasolina")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal gasto_gasolina { get; set; }

        [Display(Name = "Gasto Peajes")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal gasto_peajes { get; set; }

        [Display(Name = "Otros Gastos")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal otros_gastos { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        [StringLength(128)]
        [Display(Name = "Creado Por")]
        [UIHint("UserName")]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fec_crea { get; set; }

        [StringLength(128)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionRuta> AsignacionRutas { get; set; }

        public virtual Direccione Direccione { get; set; }

        public virtual Direccione Direccione1 { get; set; }
    }
}
