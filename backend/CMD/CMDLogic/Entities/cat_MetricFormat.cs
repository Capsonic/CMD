using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class cat_MetricFormat : BaseEntity
    {
        [NotMapped]
        public override int ID { get { return MetricFormatKey; } }
    }
}