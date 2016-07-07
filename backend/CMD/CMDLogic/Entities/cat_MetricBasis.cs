using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class cat_MetricBasis : BaseEntity
    {
        [NotMapped]
        public override int id { get { return MetricBasisKey; } }
    }
}