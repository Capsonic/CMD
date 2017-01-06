namespace CMDLogic.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MetricHistory")]
    public partial class MetricHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MetricHistory()
        {

        }

        [Key]
        public int MetricHistoryKey { get; set; }

        public string Note { get; set; }

        public decimal? CurrentValue { get; set; }

        public decimal? GoalValue { get; set; }

        public DateTime? MetricDate { get; set; }


        public int MetricYearKey { get; set; }
        public virtual MetricYear MetricYear { get; set; }
    }
}
