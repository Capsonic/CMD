namespace CMDLogic.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sort")]
    public partial class Sort
    {
        [Key]
        public int SortKey { get; set; }

        public int Sort_Entity_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Sort_Entity_Kind { get; set; }

        public int Sort_User_ID { get; set; }

        public DateTime? Sort_Edited_On { get; set; }

        public int? Sort_Sequence { get; set; }

        public int? Sort_ManyToMany_ID { get; set; }

        public virtual User User { get; set; }
    }
}
