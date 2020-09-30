using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.DataAccessLayer;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{  
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {

        private CareerCloudContext _context;

        public EFGenericRepository()
        {
            _context = new CareerCloudContext();

        }

        public void Add(params T[] items)
        {

            foreach (var item in items)
            {
                
                _context.Entry(item).State = EntityState.Added;
            }

            _context.SaveChanges();

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.Where(where).ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navProp in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navProp);
            }
            return dbQuery.FirstOrDefault(where);
        }

        public void Remove(params T[] items)
        {

            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();

        }

        public void Update(params T[] items)
        {

            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();

        }
    }
}
