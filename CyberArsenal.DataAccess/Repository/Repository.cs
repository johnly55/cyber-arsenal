using CyberArsenal.DataAccess.Data;
using CyberArsenal.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CyberArsenal.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T obj)
        {
            dbSet.Add(obj);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string properties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(properties != null)
            {
                foreach (string prop in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(prop);
                }
            }

            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string properties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (properties != null)
            {
                foreach (string prop in properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.ToList();
        }

        public bool Remove(int id)
        {
            var obj = dbSet.Find(id);

            if(obj != null)
            {
                dbSet.Remove(obj);
                return true;
            }
            return false;
        }

        public void Remove(T obj)
        {
            dbSet.Remove(obj);
        }

        public void RemoveRange(IEnumerable<T> objList)
        {
            dbSet.RemoveRange(objList);
        }
    }
}
