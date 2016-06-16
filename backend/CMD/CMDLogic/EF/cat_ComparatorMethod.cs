namespace CMDLogic.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class cat_ComparatorMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cat_ComparatorMethod()
        {
            Metrics = new HashSet<Metric>();
        }

        [Key]
        public int ComparatorMethodKey { get; set; }

        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Metric> Metrics { get; set; }
    }
}
