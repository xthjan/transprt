namespace Transprt.Data {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MenuByArea")]
    public partial class MenuByArea
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_menu { get; set; }

        [Key]
        [Column(Order = 1)]
        public string id_area { get; set; }

        public virtual Menu Menu { get; set; }

        [NotMapped]
        public bool Asignado { get; set; }
        [NotMapped]
        public string Nombre { get; set; }
    }
}
