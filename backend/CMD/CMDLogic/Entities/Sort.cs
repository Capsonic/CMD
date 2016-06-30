using CMDLogic.Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Sort : BaseEntity
    {
        [NotMapped]
        public override int ID { get { return SortKey; } }
    }
}