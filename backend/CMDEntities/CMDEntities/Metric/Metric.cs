using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Metric : Trackable, Sortable
    {
        public string Description { get; set; }
        public decimal? CurrentValue { get; set; }
        public decimal? GoalValue { get; set; }
        public long? FormatKey { get; set; }
        public long? BasisKey { get; set; }
        public long? ComparatorMethodKey { get; set; }

        //Sortable Implementation:
        public Sort SortInfo { get; set; }

        //Aux Cross:
        public long MyProperty { get; set; }
    }

    class MetricCatalogs
    {
        public List<cat_ComparatorMethod> ComparatorMethod { get; set; }
        public List<cat_MetricBasis> MetricBasis { get; set; }
        public List<cat_MetricFormat> MetricFormat { get; set; }
    }

    class cross_Objective_Metric: Sort
    {
        public long ObjectiveKey { get; set; }
        public long MetricKey { get; set; }
    }
}
