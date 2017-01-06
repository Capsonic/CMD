namespace CMDLogic.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Metric")]
    public partial class Metric
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Metric()
        {
            Departments = new HashSet<Department>();
            MetricYears = new HashSet<MetricYear>();
            sys_active = true;
        }

        [Key]
        public int MetricKey { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? CurrentValue { get; set; }

        public decimal? GoalValue { get; set; }

        public int? FormatKey { get; set; }

        public int? BasisKey { get; set; }

        public int? ComparatorMethodKey { get; set; }

        public string HiddenForDashboards { get; set; }

        public virtual cat_ComparatorMethod cat_ComparatorMethod { get; set; }

        public virtual cat_MetricBasis cat_MetricBasis { get; set; }

        public virtual cat_MetricFormat cat_MetricFormat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<MetricYear> MetricYears { get; set; }
    }
}
