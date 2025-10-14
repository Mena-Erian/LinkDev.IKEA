using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Contracts.Repositories.BaseRepositories;
using LinkDev.IKEA.DAL.Persistence.Common;
using LinkDev.IKEA.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinkDev.IKEA.DAL.Persistence.Repositories.BaseRepositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null, bool withTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);


            query = query.Where(filter);

            if (orderBy is not null)
                query = orderBy(query);

            if (withTracking)
                return await query.ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<PaginatedResult<TEntity>> GetAllAsync(QueryParameters queryParameters, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null, bool withTracking = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);

            if (filter is not null)
                query = query.Where(filter);

            var totalCount = query.Count();

            // Apply Ordering
            if (orderBy is not null)
                query = orderBy(query);

            //  Page Index = 3;
            //  Page Size = 20;
            //  
            // (pageIndex-1)*pageSize
            // skip 40;


            //Apply Pagination
            query.Skip((queryParameters.PageIndex - 1) * queryParameters.PageSize)
                 .Take(queryParameters.PageSize);

            if (withTracking)
                query.ToList();

            await query.AsNoTracking().ToListAsync();

            return new PaginatedResult<TEntity>()
            {
                Data = query,
                PageIndex = queryParameters.PageIndex,
                PageSize = queryParameters.PageSize,
                TotalCount = totalCount,
            };
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes is not null)
                query = includes(query);

            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<TEntity>> GetAll(bool withTracking = false)
        //{
        //    if (withTracking) return _dbSet;
        //    return await _dbSet.AsNoTracking().ToListAsync();
        //}

        public IQueryable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking) return _dbSet;
            return _dbSet.AsNoTracking();
        }
        public async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Update(TEntity entity)
        {
            var localEntity = _dbSet.Local.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (localEntity != null)
            {

                _dbSet.Entry(localEntity).State = EntityState.Detached;
            }

            _dbSet.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity is { })
                _dbSet.Remove(entity);
        }
        public bool Exists(Expression<Func<TEntity, bool>> filter) => _dbSet.Any(filter);
 
    }
}
