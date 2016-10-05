using Reusable;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Metric : BaseDocument
    {
        [NotMapped]
        public override int id { get { return MetricKey; } }

        [NotMapped]
        public Sort InfoSort { get; set; }
    }
}