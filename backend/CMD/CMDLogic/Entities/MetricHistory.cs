using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class MetricHistory : BaseEntity
    {
        [NotMapped]
        public override int id { get { return MetricHistoryKey; } }
    }
}