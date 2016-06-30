using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using static CMDLogic.Reusable.BaseEntity;

namespace CMDLogic.Reusable
{
    public class BaseEntityRepository<T> : BaseRepository<T>, IEntityRepository<T> where T : BaseEntity
    {
        protected static EntityState GetEntityState(EF_EntityState state)
        {
            switch (state)
            {
                case EF_EntityState.Unchanged:
                    return EntityState.Unchanged;
                case EF_EntityState.Added:
                    return EntityState.Added;
                case EF_EntityState.Modified:
                    return EntityState.Modified;
                case EF_EntityState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Detached;
            }
        }

        public override void Add(params T[] items)
        {
            DbSet<T> dbSet = context.Set<T>();
            foreach (T item in items)
            {
                dbSet.Attach(item);
            }

            foreach (DbEntityEntry<BaseEntity> entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                context.Entry(entry.Entity).State = EntityState.Unchanged;
            }

            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Added;
            }
            context.SaveChanges();
        }

        public void AddToParent<P>(int parentId, T entity) where P : BaseEntity
        {
            DbSet<P> parentSet = context.Set<P>();
            P parent = parentSet.Find(parentId);
            if (parent == null)
            {
                throw new Exception("Non-existent Parent Entity.");
            }
            if (parent is BaseDocument)
            {
                if ((parent as BaseDocument).sys_active == false)
                {
                    throw new Exception("Non-existent Parent Entity.");
                }
            }

            context.Entry(parent).State = EntityState.Unchanged;

            string navigationPropertyName = typeof(P).Name + "s";

            DbSet<T> entitySet = context.Set<T>();
            entitySet.Attach(entity);

            PropertyInfo navigationProperty = entity.GetType().GetProperty(navigationPropertyName, BindingFlags.Public | BindingFlags.Instance);
            ICollection<P> parentsList = (ICollection<P>)navigationProperty.GetValue(entity);

            parentsList.Add(parent);

            if (entity.ID > 0)
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
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
