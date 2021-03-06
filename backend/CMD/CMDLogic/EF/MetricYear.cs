namespace CMDLogic.EF
{
    using Reusable;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MetricYear")]
    public partial class MetricYear : BaseDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetricYear()
        {
            MetricHistorys = new HashSet<MetricHistory>();
            sys_active = true;
        }

        [Key]
        public int MetricYearKey { get; set; }

        public string Note { get; set; }

        public int Value { get; set; }

        public decimal? CurrentValue { get; set; }

        public decimal? GoalValue { get; set; }

        public decimal? AroundRangeValue { get; set; }

        public int? FormatKey { get; set; }

        public int? BasisKey { get; set; }

        public int? ComparatorMethodKey { get; set; }

        public string HiddenForDashboards { get; set; }

        public virtual cat_ComparatorMethod cat_ComparatorMethod { get; set; }

        public virtual cat_MetricBasis cat_MetricBasis { get; set; }

        public virtual cat_MetricFormat cat_MetricFormat { get; set; }

        public int MetricKey { get; set; }
        public virtual Metric Metric { get; set; }

        public virtual ICollection<MetricHistory> MetricHistorys { get; set; }

        [NotMapped]
        public override int id { get { return MetricYearKey; } }

    }
}
