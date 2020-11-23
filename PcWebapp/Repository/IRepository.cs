using System;
using System.Linq;

namespace Repository
{
    public interface IRepository<T> where T : new()
    {
        void Add(T item);
        void Delete(T item);
        void Delete(string uid);
        T Read(string uid);
        IQueryable<T> Read();
        void Update(string oldid, T newitem);
        void Save();
    }
}
