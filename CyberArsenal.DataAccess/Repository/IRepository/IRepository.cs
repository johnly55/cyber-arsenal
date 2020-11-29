using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CyberArsenal.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Add(T obj);

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string properties = null);

        public T Get(int id);

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string properties = null);

        public bool Remove(int id);

        public void Remove(T obj);

        public void RemoveRange(IEnumerable<T> objList);
    }
}
