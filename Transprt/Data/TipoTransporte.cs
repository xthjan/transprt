namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoTransporte")]
    public partial class TipoTransporte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoTransporte()
        {
            Transportes = new HashSet<Transporte>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Modelo")]
        public int id_modelo { get; set; }

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
        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        [Display(AutoGenerateField = false)]
        [Editable(false)]
        public DateTime? fec_modif { get; set; }
        [Display(Name = "Modelo")]
        public virtual Modelo Modelo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transporte> Transportes { get; set; }
    }
}
