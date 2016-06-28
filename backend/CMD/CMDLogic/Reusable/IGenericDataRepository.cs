using CMDLogic.Reusable;
using System;
using System.Collections.Generic;

namespace CMDLogic
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetList(Func<T, bool> where);
        T GetByID(int ID);
        T GetSingle(Func<T, bool> where);
        void Add(params T[] items);
        void Update(params T[] items);
        void Delete(params T[] items);
    }

    public interface IGenericEntityRepository<T> : IGenericDataRepository<T> where T : class
    {
        IList<T> GetListByParent<P>(int parentID) where P : BaseEntity;
        T GetSingleByParent<P>(int parentID) where P : BaseEntity;
    }

    public interface IGenericDocumentRepository<T> : IGenericEntityRepository<T> where T : class
    {
        void Activate(params T[] items);
        void Deactivate(params T[] items);
    }
}
