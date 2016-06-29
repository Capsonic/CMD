using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMDLogic.Reusable
{   
    public abstract class BaseEntity : ICloneable
    {
        [NotMapped]
        public abstract int ID { get; }
        
        [NotMapped]
        public string AAA_EntityName { get { return GetType().Name.Split('_')[0]; } }        

        public override bool Equals(object obj)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(obj, null)) return false;

            //Check whether the compared object is same type.
            if (!this.GetType().Equals(obj.GetType())) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, obj)) return true;

            //Check whether the IEntity' ids are equal.
            return ID.Equals(((BaseEntity)obj).ID);
        }
        public override int GetHashCode()
        {
            //Get hash code for the id field if it is not null.
            int hashID = ID.GetHashCode();

            return hashID;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
