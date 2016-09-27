namespace Transprt.Data {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transporte {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transporte() {
            AsignacionRutas = new HashSet<AsignacionRuta>();
        }

        public int id { get; set; }

        [StringLength(50)]
        [Display(Name = "Clave")]
        public string nombre { get; set; }
        [Display(Name = "Tipo de Transporte")]
        public int? id_tipo_transporte { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha de adquisición")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fec_adquisicion { get; set; }

        [StringLength(10)]
        [Display(Name = "Valor Factura")]
        public string valor_factura { get; set; }

        [StringLength(10)]
        [Display(Name = "Matrícula")]
        public string matricula { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha última Inspección")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fec_ultima_inspeccion { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha último servicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? fec_ultimo_servicio { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; }

        [StringLength(128)]
        [Display(Name = "Creado Por")]
        [UIHint("UserName")]
        [Editable(false)]
        public string usr_crea { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionRuta> AsignacionRutas { get; set; }

        public virtual TipoTransporte TipoTransporte { get; set; }
    }
}
