namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pedidos")]
    public partial class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int id_cliente { get; set; }

        public int id_dir_recogida { get; set; }

        public int id_dir_entrega { get; set; }

        public int id_cliente_entrega { get; set; }

        public int? id_asignacion_ruta { get; set; }

        [Required]
        [StringLength(10)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        public DateTime fec_crea { get; set; }

        [StringLength(10)]
        public string usr_modif { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fec_modif { get; set; }

        public virtual AsignacionRuta AsignacionRuta { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Cliente Cliente1 { get; set; }

        public virtual Direccione Direccione { get; set; }

        public virtual Direccione Direccione1 { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Usuario Usuario1 { get; set; }
    }
}
