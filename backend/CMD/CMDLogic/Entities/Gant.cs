﻿using Reusable;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.EF
{
    public partial class Gant : BaseDocument
    {
        [NotMapped]
        public override int id { get { return GantKey; } }
    }
}