namespace CMDLogic.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Track")]
    public partial class Track
    {
        [Key]
        public int TrackKey { get; set; }

        public int Entity_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Entity_Kind { get; set; }

        public int User_CreatedBy { get; set; }

        public DateTime Date_CreatedOn { get; set; }

        public DateTime? Date_EditedOn { get; set; }

        public DateTime? Date_RemovedOn { get; set; }

        public DateTime? Date_LastTimeUsed { get; set; }

        public int? User_LastEditedBy { get; set; }

        public int? User_RemovedBy { get; set; }

        public int? User_AssignedTo { get; set; }

        public int? User_AssignedBy { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual User User2 { get; set; }

        public virtual User User3 { get; set; }
    }
}
