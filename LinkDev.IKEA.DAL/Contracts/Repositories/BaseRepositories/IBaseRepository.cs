using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Entities;
using LinkDev.IKEA.DAL.Persistence.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Contracts.Repositories.BaseRepositories
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
            bool withTracking = false
            );

        PaginatedResult<TEntity> GetAll(
            QueryParameters queryParameters,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null,
            bool withTracking = false
            );

        TEntity? Get(int id);

        TEntity? Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);

        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}
