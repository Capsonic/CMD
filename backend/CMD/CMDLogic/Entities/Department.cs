using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Department : BaseDocument
    {
        [NotMapped]
        public override int id { get { return DepartmentKey; } }

        [NotMapped]
        public Gridster InfoGridster { get; set; }
    }
}