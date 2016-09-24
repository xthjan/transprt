namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AsignacionRuta")]
    public partial class AsignacionRuta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AsignacionRuta()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int id { get; set; }

        public int id_ruta { get; set; }

        public int id_chofer { get; set; }

        public int id_transporte { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_final { get; set; }

        [StringLength(128)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_crea { get; set; }

        [StringLength(128)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        public virtual Chofere Chofere { get; set; }

        public virtual Ruta Ruta { get; set; }

        public virtual Transporte Transporte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
