using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Objective : Trackable
    {
        public string Title { get; set; }

        public List<Metric> MetricsList { get; set; }
        public List<Initiative> InitiativesList { get; set; }
    }

    class cross_Objective_Sequence : IEntity
    {

    }
}
