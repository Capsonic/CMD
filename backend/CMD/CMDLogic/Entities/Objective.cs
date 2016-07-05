using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Objective : BaseDocument
    {
        [NotMapped]
        public override int ID { get { return ObjectiveKey; } }
    }
}