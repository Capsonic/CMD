using CMDLogic.Reusable;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CMDLogic.EF
{
    public partial class Objective : BaseDocument
    {
        [NotMapped]
        public override int ID { get { return ObjectiveKey; } }
    }
}