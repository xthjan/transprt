namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Modelos")]
    public partial class Modelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modelo()
        {
            TipoTransportes = new HashSet<TipoTransporte>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Año Fabricación")]
        public int anho_fabricacion { get; set; }

        public int id_marca { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        [StringLength(128)]
        [Display(Name = "Creado Por")]
        [UIHint("UserName")]
        [Editable(false)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Editable(false)]
        public DateTime fec_crea { get; set; }

        [StringLength(128)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        public virtual Marca Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoTransporte> TipoTransportes { get; set; }
    }
}
