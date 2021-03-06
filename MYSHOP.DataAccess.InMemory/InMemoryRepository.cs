using MYSHOP.CORE.Contracts;
using MYSHOP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace MYSHOP.DataAccess.InMemory
{
    public class InmemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InmemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }

        
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToupdate = items.Find(i => i.Id == t.Id);

            if (tToupdate != null)
            {
                tToupdate = t;
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }
        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }
        public IQueryable<T> collection()
        {
            return items.AsQueryable();
        }
        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + "not found");
            }
        }

    }
}
