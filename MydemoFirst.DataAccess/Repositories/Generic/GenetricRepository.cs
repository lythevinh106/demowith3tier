using DTOShared.FetchData;
using DTOShared.Pagging;
using Microsoft.EntityFrameworkCore;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.Models;
using System.Linq.Expressions;

namespace MydemoFirst.DataAccess.Repositories.Generic
{
    public abstract class GenetricRepository<T> : IRepository<T> where T : class
    {
        protected MyDemoFirstWith3TierContext _context;

        public GenetricRepository(MyDemoFirstWith3TierContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            var result = await _context.AddAsync(entity);
            return result.Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual IQueryable<T> All()
        {
            return _context.Set<T>();
        }

        public virtual async Task<T> Get(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public virtual T Update(T entity)
        {
            return _context.Update<T>(entity).Entity;
        }

        public PagedList<T> FectchData(FetchDataRequest fetchDataRequest, IQueryable<T> query)
        {


            return PagedList<T>
                .ToPagedList(query, fetchDataRequest.PageNumber, fetchDataRequest.PageSize);
        }



        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate, IQueryable<T> query)
        {
            return query.Where(predicate);
        }

        public IQueryable<T> Sort(Expression<Func<T, string>> field, IQueryable<T> query, bool ascending = true)
        {
            if (ascending) return query.OrderBy(field);

            return query.OrderByDescending(field);
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate, IQueryable<T> query)
        {


            return query.Where(predicate);
        }


        public async Task<bool> CheckExist(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public T Delete(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }



        // abstract public IQueryable<T> Search(IQueryable<T> ListItem, string requestSearch);
    }
}