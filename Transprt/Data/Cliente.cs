namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
            Pedidos1 = new HashSet<Pedido>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string nom_razon { get; set; }

        public int id_persona_contacto { get; set; }

        [Required]
        [StringLength(15)]
        public string rfc { get; set; }

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

        public virtual Persona Persona { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedido> Pedidos1 { get; set; }
    }
}
