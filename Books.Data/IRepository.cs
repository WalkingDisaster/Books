using System;
using System.Collections.Generic;

namespace Books.Data
{
    public interface IRepository<T>
        where T : class
    {
        T Insert(T toInsert);
        T Get(int id);
        void Update(T toUpdate);
        IEnumerable<T> Query(Func<T, bool> func);
    }
}