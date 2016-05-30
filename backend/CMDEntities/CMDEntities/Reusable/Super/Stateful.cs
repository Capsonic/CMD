using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Super
{
    interface Stateful
    {
        string Entity_Status { get; set; } // CREATED, IN PROGRESS, COMPLETED
        bool Entity_IsLocked { get; set; }
    }
}
