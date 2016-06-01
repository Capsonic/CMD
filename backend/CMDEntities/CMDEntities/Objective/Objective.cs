using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Objective : Trackable, Sortable
    {
        public string Title { get; set; }

        public List<Metric> MetricsList { get; set; }
        public List<Initiative> InitiativesList { get; set; }

        //Sortable Implementation:
        public Sort SortInfo { get; set; }

        //Aux Cross:
        public long? DashboardKey { get; set; }
    }

    class cross_Dashboard_Objective: Sort
    {
        public long DashboardKey { get; set; }
        public long ObjectiveKey { get; set; }
    }
}
