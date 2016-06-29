using CMDLogic.Reusable;
using System;
using System.Collections.Generic;

namespace CMDLogic
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetList(Func<T, bool> where);
        T GetByID(int id);
        T GetSingle(Func<T, bool> where);
        void Add(params T[] items);
        void Update(params T[] items);
        void Delete(int id);
    }

    public interface IEntityRepository<T> : IRepository<T> where T : BaseEntity
    {
        IList<T> GetListByParent<P>(int parentID) where P : BaseEntity;
        T GetSingleByParent<P>(int parentID) where P : BaseEntity;
        void AddToParent<P>(int parentId, T entity) where P : BaseEntity;
    }

    public interface IDocumentRepository<T> : IEntityRepository<T> where T : BaseDocument
    {
        void Activate(int id);
        void Deactivate(int id);
    }
}
