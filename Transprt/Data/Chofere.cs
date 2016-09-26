namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chofere
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chofere()
        {
            AsignacionRutas = new HashSet<AsignacionRuta>();
        }

        public int id { get; set; }

        public int id_persona { get; set; }

        [Display(Name = "Edad")]
        public int edad { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "NSS")]
        public string nss { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Licencia")]
        public string num_lic_conducir { get; set; }

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

        public virtual Persona Persona { get; set; }
    }
}
