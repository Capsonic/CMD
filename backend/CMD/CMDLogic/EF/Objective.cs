namespace CMDLogic.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Objective")]
    public partial class Objective
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Objective()
        {
            Dashboards = new HashSet<Dashboard>();
            Initiatives = new HashSet<Initiative>();
            Metrics = new HashSet<Metric>();
            sys_active = true;
        }

        [Key]
        public int ObjectiveKey { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        public bool sys_active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dashboard> Dashboards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Initiative> Initiatives { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Metric> Metrics { get; set; }
    }
}
