using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GroupHub.Core
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, IIdentifiableEntity, new()
    {
        T Add(T entity);

        IEnumerable<T> Add(IEnumerable<T> entityList);

        void Remove(T entity);

        void Remove(int id);

        T Update(T entity);

        T Get(int id);

        IEnumerable<T> Get();

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        //IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] navigationProperties);

        //IPagedResult<T> GetPagedResult(int pageIndex, int pageSize);
        //IPagedResult<T> GetPagedResult(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate);
        //IPagedResult<T> GetPagedResult(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] navigationProperties);
    }
}
