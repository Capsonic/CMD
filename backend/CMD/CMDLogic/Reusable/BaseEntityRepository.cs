using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CMDLogic.Reusable
{
    public class BaseEntityRepository<T> : BaseRepository<T>, IEntityRepository<T> where T : BaseEntity
    {
        public void AddToParent<P>(int parentId, T entity) where P : BaseEntity
        {
            //TODO
        }

        public virtual IList<T> GetListByParent<P>(int parentID) where P : BaseEntity
        {
            List<T> list = new List<T>();

            DbSet<P> setParent = context.Set<P>();

            P parent = setParent.Find(parentID);

            if (parent == null)
            {
                throw new Exception("Parent non-existent.");
            }

            if (parent is BaseDocument)
            {
                if ((parent as BaseDocument).sys_active == false)
                {
                    throw new Exception("Parent non-existent.");
                }
            }

            string tName = typeof(T).Name + "s";
            list = context.Entry(parent).Collection<T>(tName)
                .Query()
                .AsNoTracking()
                .ToList<T>();

            return list;
        }

        public virtual T GetSingleByParent<P>(int parentID) where P : BaseEntity
        {
            T entity = null;

            DbSet<P> setParent = context.Set<P>();

            P parent = setParent.Find(parentID);

            if (parent == null)
            {
                throw new Exception("Parent non-existent.");
            }

            if (parent is BaseDocument)
            {
                if ((parent as BaseDocument).sys_active == false)
                {
                    throw new Exception("Parent non-existent.");
                }
            }

            string tName = typeof(T).Name;
            entity = context.Entry(parent).Reference<T>(tName).Query().FirstOrDefault();
            
            return entity;
        }
    }
}
