using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Dashboard : Trackable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Objective> ObjectivesList { get; set; }
    }
}
