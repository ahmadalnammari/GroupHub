using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace GroupHub.Core
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
        where T : class, IIdentifiableEntity, new()
        where U : DbContext, new()
    {

        protected virtual T AddEntity(U entityContext, T entity)
        {
            return entityContext.Set<T>().Add(entity);
        }

        protected virtual T UpdateEntity(U entityContext, T entity)
        {
            entityContext.Entry<T>(entity).State = EntityState.Modified;
            return entity;
        }

        protected virtual T GetEntity(U entityContext, int id)
        {
            return entityContext.Set<T>().Where(o => o.Id == id).ToFullyLoaded().SingleOrDefault();
        }

        protected virtual IQueryable<T> GetEntities(U entityContext)
        {
            return entityContext.Set<T>().AsQueryable<T>();
        }

        public T Add(T entity)
        {
            using (U entityContext = new U())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public IEnumerable<T> Add(IEnumerable<T> entityList)
        {
            using (U entityContext = new U())
            {
                IList<T> addedEntityList = new List<T>();
                DbSet<T> dbSet = entityContext.Set<T>();
                foreach (T entity in entityList)
                {
                    T addedEntity = AddEntity(entityContext, entity);
                    addedEntityList.Add(addedEntity);
                }
                entityContext.SaveChanges();
                return addedEntityList;
            }
        }

        public void Remove(T entity)
        {
            using (U entityContext = new U())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (U entityContext = new U())
            {
                T entity = Get(id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            using (U entityContext = new U())
            {
                T modifiedEntity = UpdateEntity(entityContext, entity);
                entityContext.SaveChanges();

                return modifiedEntity;
            }
        }

        #region Get methods
        public T Get(int id)
        {
            using (U entityContext = new U())
            {
                return GetEntity(entityContext, id);
            }
        }
        public IEnumerable<T> Get()
        {
            using (U entityContext = new U())
            {
                return GetEntities(entityContext).ToFullyLoaded();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (U entityContext = new U())
            {
                return GetEntities(entityContext).Where(predicate).ToFullyLoaded();
            }
        }

        //public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] navigationProperties)
        //{
        //    using (U entityContext = new U())
        //    {
        //        return Get(entityContext, predicate, orderBy, navigationProperties).AsEnumerable();
        //    }
        //}

        #region Get pages results

        //public IPagedResult<T> GetPagedResult(int pageIndex, int pageSize)
        //{
        //    return GetPagedResult(pageIndex, pageSize, x => true);
        //}

        //public IPagedResult<T> GetPagedResult(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate)
        //{
        //    return GetPagedResult(pageIndex, pageSize, predicate, x => x.Id);
        //}

        //public IPagedResult<T> GetPagedResult(int pageIndex, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] navigationProperties)
        //{
        //    using (U entityContext = new U())
        //    {
        //        var query = Get(entityContext, predicate, orderBy, navigationProperties);

        //        IPagedResult<T> result = new PagedResult<T>();
        //        result.CurrentPage = pageIndex + 1;
        //        result.PageSize = pageSize;
        //        result.TotalCount = query.Count();
        //        result.TotalPagesCount = (result.TotalCount + pageSize - 1) / pageSize;
        //        result.Results = query.Skip(pageIndex * pageSize).Take(pageSize).ToFullyLoaded();

        //        return result;
        //    }


        //}
        #endregion

        #endregion


        //protected IQueryable<T> Get(U entityContext, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params Expression<Func<T, object>>[] navigationProperties)
        //{
        //    var query = GetEntities(entityContext);

        //    if (navigationProperties != null)
        //    {
        //        foreach (var navigationProperty in navigationProperties)
        //            query = query.Include(navigationProperty);
        //    }

        //    return query.Where(predicate).OrderBy(orderBy);
        //}

    }
}