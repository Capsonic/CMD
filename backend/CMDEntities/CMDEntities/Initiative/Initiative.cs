﻿using CMDEntities.Reusable;
using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities
{
    class Initiative : Trackable, Sortable
    {
        public decimal? ProgressValue { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ActualDate { get; set; }

        public Gant TheGant { get; set; }

        //Sortable Implementation:
        public Sort SortInfo { get; set; }

        //Aux Cross:
        public long? ObjectiveKey { get; set; }
    }

    class cross_Objective_Initiative : Sort
    {
        public long ObjectiveKey { get; set; }
        public long InitiativeKey { get; set; }
    }
}
