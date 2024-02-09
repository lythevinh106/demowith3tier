using DTOShared.FetchData;
using DTOShared.Pagging;
using System.Linq.Expressions;

namespace MydemoFirst.DataAccess.Infrastructure
{
    public interface IRepository<T>
    {


        public Task<bool> CheckExist(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);

        T Update(T entity);

        Task<T> Get(int id);

        T Delete(T entity);
        IQueryable<T> All();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);


        PagedList<T> FectchData(FetchDataRequest fetchDataRequest, IQueryable<T> query);

        IQueryable<T> Filter(Expression<Func<T, bool>> predicate, IQueryable<T> query);


        IQueryable<T> Sort(Expression<Func<T, string>> field, IQueryable<T> query, bool ascending = true);

        IQueryable<T> Search(Expression<Func<T, bool>> predicate, IQueryable<T> query);

    }
}
