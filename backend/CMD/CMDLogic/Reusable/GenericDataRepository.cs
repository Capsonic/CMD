using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMDLogic
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual void Add(DbContext context, int? byUserID, params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Added;
            }
            context.SaveChanges();
        }

        public virtual IList<T> GetAll(DbContext context, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();
            return list;
        }

        public virtual IList<T> GetList(DbContext context, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();

            return list;
        }

        public virtual T GetSingle(DbContext context, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            item = dbQuery
                .AsNoTracking()
                .FirstOrDefault(where);

            return item;
        }

        public virtual void Remove(DbContext context, int? byUserID, params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
        }

        public virtual void Update(DbContext context, int? byUserID, params T[] items)
        {
            foreach (T item in items)
            {
                context.Entry(item).State = EntityState.Modified;
            }
        }

        //public virtual void SetActive(DbContext context, bool bActive, params T[] items)
        //{
        //    foreach (T item in items)
        //    {
                
        //        context.Entry(item).State = EntityState.Modified;
        //    }
        //}
    }
}
