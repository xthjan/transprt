namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
