using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace CMDLogic
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public virtual DbContext context { get; set; }

        public virtual int? byUserID { get; set; }

        public virtual void Add(params T[] items)
        {
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
    }
}
