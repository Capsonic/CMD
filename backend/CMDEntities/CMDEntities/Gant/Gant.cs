using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Gant : Trackable
    {
        public long InitiativeKey { get; set; }
        public string GantData { get; set; }
    }
}
