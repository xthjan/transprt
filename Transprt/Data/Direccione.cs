namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Direccione
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Direccione()
        {
            Pedidos = new HashSet<Pedido>();
            Pedidos1 = new HashSet<Pedido>();
            Rutas = new HashSet<Ruta>();
            Rutas1 = new HashSet<Ruta>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string calle { get; set; }

        [Required]
        [StringLength(50)]
        public string num_ext { get; set; }

        [StringLength(50)]
        public string num_int { get; set; }

        [Required]
        [StringLength(50)]
        public string col { get; set; }

        public int id_estado { get; set; }

        [Required]
        [StringLength(10)]
        public string cp { get; set; }

        [Required]
        [StringLength(50)]
        public string tel_fijo { get; set; }

        public bool activo { get; set; }

        [StringLength(128)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_crea { get; set; }

        [StringLength(128)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        public virtual Estado Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidos1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ruta> Rutas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ruta> Rutas1 { get; set; }
    }
}
