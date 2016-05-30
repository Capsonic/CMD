using CMDEntities.Reusable.Super;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Catalogs
{
    abstract class Catalog : IEntity
    {
        public string Value { get; set; }
    }
}
