namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transporte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transporte()
        {
            AsignacionRutas = new HashSet<AsignacionRuta>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        public int? id_tipo_transporte { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_adquisicion { get; set; }

        [StringLength(10)]
        public string valor_factura { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_ultima_inspeccion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_ultimo_servicio { get; set; }

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

        public virtual TipoTransporte TipoTransporte { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }
    }
}
