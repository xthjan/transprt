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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        public int id_modelo { get; set; }

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

        public virtual Modelo Modelo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transporte> Transportes { get; set; }
    }
}
