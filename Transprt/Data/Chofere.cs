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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int id_persona { get; set; }

        public int edad { get; set; }

        [Required]
        [StringLength(1)]
        public string sexo { get; set; }

        [Required]
        [StringLength(15)]
        public string nss { get; set; }

        [Required]
        [StringLength(15)]
        public string num_lic_conducir { get; set; }

        public bool activo { get; set; }

        [Required]
        [StringLength(10)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_crea { get; set; }

        [StringLength(10)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionRuta> AsignacionRutas { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }
    }
}
