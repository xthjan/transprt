namespace Transprt.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuByRole")]
    public partial class MenuByRole
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_menu { get; set; }

        [Key]
        [Column(Order = 1)]
        public string id_role { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
