using CMDLogic.Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Dashboard : BaseDocument
    {
        [NotMapped]
        public override int ID { get { return DashboardKey; } }
    }
}