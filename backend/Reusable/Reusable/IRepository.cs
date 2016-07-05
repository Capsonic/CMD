﻿using System;
using System.Collections.Generic;

namespace Reusable
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

        IList<T> GetListByParent<P>(int parentID) where P : class;
        T GetSingleByParent<P>(int parentID) where P : class;
        void AddToParent<P>(int parentId, T entity) where P : class;

        void Activate(int id);
        void Deactivate(int id);

        ITrack track { get; set; }
    }
}
