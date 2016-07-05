using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Reusable
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;
        protected readonly int? byUserId;

        protected readonly BaseRepository<ITrack> _trackRepository;

        public ITrack track { get; set; }

        public BaseRepository(DbContext context, ITrack track, int? byUserId = 1)
        {
            this.context = context;
            this.track = track;
            this.byUserId = byUserId;
        }

        public virtual void Add(params T[] items)
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

        public virtual IList<T> GetAll()
        {
            List<T> list;
            IQueryable<T> dbQuery = context.Set<T>();

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where)
        {
            List<T> list;
            IQueryable<T> dbQuery = context.Set<T>();

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();

            return list;
        }

        public virtual T GetSingle(Func<T, bool> where)
        {
            T item = null;
            IQueryable<T> dbQuery = context.Set<T>();

            item = dbQuery
                .AsNoTracking()
                .FirstOrDefault(where);

            return item;
        }

        public virtual void Delete(int id)
        {
            DbSet<T> tSet = context.Set<T>();
            T entity = tSet.Find(id);

            if (entity != null)
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Entity not found.");
            }
        }

        public virtual void Update(params T[] items)
        {
            DbSet<T> dbSet = context.Set<T>();
            foreach (T item in items)
            {
                //dbSet.Attach(item);
                context.Entry(item).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public virtual T GetByID(int id)
        {
            DbSet<T> set = context.Set<T>();

            return set.Find(id);
        }

        public virtual IList<T> GetListByParent<P>(int parentID) where P : class
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

        public void AddToParent<P>(int parentId, T entity) where P : class
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

            string sPropID = typeof(T).Name + "Key";
            int id = (int) context.Entry(entity).Property(sPropID).CurrentValue;

            if (id > 0)
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
        }

        public virtual T GetSingleByParent<P>(int parentID) where P : class
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

    //protected static EntityState GetEntityState(EF_EntityState state)
    //{
    //    switch (state)
    //    {
    //        case EF_EntityState.Unchanged:
    //            return EntityState.Unchanged;
    //        case EF_EntityState.Added:
    //            return EntityState.Added;
    //        case EF_EntityState.Modified:
    //            return EntityState.Modified;
    //        case EF_EntityState.Deleted:
    //            return EntityState.Deleted;
    //        default:
    //            return EntityState.Detached;
    //    }
    //}
}
