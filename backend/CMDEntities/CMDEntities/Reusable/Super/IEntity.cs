using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Super
{
    abstract class IEntity : ICloneable
    {
        public long id { get; set; }

        //To be removed when all is done for production.
        public string AAA_EntityName { get; set; }
        public IEntity()
        {
            AAA_EntityName = this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(obj, null)) return false;

            //Check whether the compared object is same type.
            if (!this.GetType().Equals(obj.GetType())) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, obj)) return true;

            //Check whether the IEntity' ids are equal.
            return id.Equals(((IEntity)obj).id);
        }
        public override int GetHashCode()
        {
            //Get hash code for the id field if it is not null.
            int hashID = id.GetHashCode();

            return hashID;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
