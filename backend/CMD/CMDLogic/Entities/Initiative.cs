using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Initiative : BaseDocument
    {
        [NotMapped]
        public override int id { get { return InitiativeKey; } }

        [NotMapped]
        public Sort InfoSort { get; set; }
    }
}