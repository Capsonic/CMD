using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Initiative : Trackable
    {
        public decimal? ProgressValue { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ActualDate { get; set; }

        public Gant TheGant { get; set; }
    }
}
