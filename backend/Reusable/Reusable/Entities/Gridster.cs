namespace Reusable
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Gridster")]
    public partial class Gridster : BaseEntity
    {
        [Key]
        public int GridsterKey { get; set; }

        public int Gridster_Entity_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Gridster_Entity_Kind { get; set; }

        public int Gridster_User_ID { get; set; }

        public DateTime? Gridster_Edited_On { get; set; }

        public int? Gridster_ManyToMany_ID { get; set; }

        public int cols { get; set; }
        public int rows { get; set; }
        public int y { get; set; }
        public int x { get; set; }

        public virtual User User { get; set; }

        public override int id { get { return GridsterKey; } }
    }
}
