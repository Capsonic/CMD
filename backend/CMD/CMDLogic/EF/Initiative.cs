namespace CMDLogic.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Initiative")]
    public partial class Initiative
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Initiative()
        {
            Gants = new HashSet<Gant>();
            Objectives = new HashSet<Objective>();
            sys_active = true;
        }

        [Key]
        public int InitiativeKey { get; set; }

        public decimal ProgressValue { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ActualDate { get; set; }

        public bool sys_active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gant> Gants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Objective> Objectives { get; set; }
    }
}
