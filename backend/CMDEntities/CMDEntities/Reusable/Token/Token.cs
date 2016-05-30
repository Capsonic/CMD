using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Token
{
    class Token : IEntity
    {
        public string TokenGenerated { get; set; }
        public string Subject { get; set; }
        public long SubjectKey { get; set; }
        public DateTime? DeadDate { get; set; }
        public bool sys_active { get; set; }
    }
}
