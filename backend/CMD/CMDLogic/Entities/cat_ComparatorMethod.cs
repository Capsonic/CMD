using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class cat_ComparatorMethod : BaseEntity
    {
        [NotMapped]
        public override int id { get { return ComparatorMethodKey; } }
    }
}