﻿using MYSHOP.CORE.Models;
using System.Linq;

namespace MYSHOP.CORE.Contracts { 
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}