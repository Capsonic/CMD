using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMDLogic
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(DbContext context, params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(DbContext context, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(DbContext context, Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(DbContext context, int? byUserID, params T[] items);
        void Update(DbContext context, int? byUserID, params T[] items);
        void Remove(DbContext context, int? byUserID, params T[] items);
    }

    public interface IGenericDocumentRepository<T> : IGenericDataRepository<T> where T : class
    {
        void SetActive(DbContext context, int? byUserID, bool bActive, params T[] items);
    }
}
