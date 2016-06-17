namespace CMDLogic.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Dashboard")]
    public partial class Dashboard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dashboard()
        {
            Objectives = new HashSet<Objective>();
            sys_active = true;
        }

        [Key]
        public int DashboardKey { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Objective> Objectives { get; set; }
    }
}
