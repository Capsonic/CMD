using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Dashboard : BaseDocument
    {
        [NotMapped]
        public override int id { get { return DashboardKey; } }

        public string Value { get { return Name; } }
    }
}