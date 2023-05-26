using CodeFirst.Core.Interfaces.Repositories;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Infrastructure.Extensions;
using CodeFirst.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly CodeFirstContext _dbContext;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(CodeFirstContext dbContext)
        {
            _dbContext = dbContext ?? throw new InfrastructureException(nameof(dbContext), $"El parametro dbContext no puede ser null");
            _entities = _dbContext.Set<TEntity>();
        }

        #region Query methods

        #region Asynchronous Read methods

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            )
        {
            return await Task.FromResult(
                                GetQueryable(null, orderBy, includeProperties, skip, take))
                             .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedElementsAsync<k>(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, k>> orderByExpression,
            bool ascending = true,
            string includeProperties = ""
            )
        {
            return await Task.FromResult(
                                 GetPagedElements(pageIndex, pageCount, orderByExpression, ascending, includeProperties))
                             .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            )
        {
            return await Task.FromResult(
                                GetQueryable(filter, orderBy, includeProperties, skip, take))
                             .ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
            )
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null
            )
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(params object[] pks)
        {
            if (pks == null) throw new InfrastructureException(nameof(pks), $"El parametro pks no puede ser null");
            return await _entities.FindAsync(pks);
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ExecStoreProcedureAsync(string query, params object[] parameters)
        {
            return await _entities.FromSqlRaw<TEntity>(query, parameters).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ExecStoreProcedureAsync(string query)
        {
            return await _entities.FromSqlRaw<TEntity>(query).ToListAsync();
        }

        #endregion Asynchronous Read methods

        #region Synchronous Read methods

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            )
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            )
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take);
        }

        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = ""
            )
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public virtual TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = ""
            )
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual TEntity GetById(params object[] pks)
        {
            if (pks == null) throw new InfrastructureException(nameof(pks), $"El parametro pks no puede ser null");
            return _entities.Find(pks);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            )
        {
            IQueryable<TEntity> query = _entities.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected virtual IQueryable<TEntity> GetPagedElements<k>(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, k>> orderByExpression,
            bool ascending = true,
            string includeProperties = ""
            )
        {
            IQueryable<TEntity> query = _entities.AsNoTracking();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (pageIndex < 1) { pageIndex = 1; }

            if (orderByExpression == null)
            {
                throw new InfrastructureException(nameof(orderByExpression), "El paginado no tiene un orden.");
            }

            return
                ascending
                            ?
                            query.OrderBy(orderByExpression)
                                 .Skip((pageIndex - 1) * pageCount)
                                 .Take(pageCount)
                            :
                            query.OrderByDescending(orderByExpression)
                                 .Skip((pageIndex - 1) * pageCount)
                                 .Take(pageCount)
                         ;
        }

        public virtual IEnumerable<TEntity> ExecStoreProcedure(string query, params object[] parameters)
        {
            return _entities.FromSqlRaw<TEntity>(query, parameters).ToList();
        }

        public virtual IEnumerable<TEntity> ExecStoreProcedure(string query)
        {
            return _entities.FromSqlRaw<TEntity>(query).ToList();
        }

        #endregion Synchronous Read methods

        #endregion Query methods

        #region Crud methods

        #region Asynchronous methods

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);

            await _entities.AddAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public async Task<bool> AddRangeAsync(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            await _entities.AddRangeAsync(entity).ConfigureAwait(false);
            return true;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);
            _entities.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            _entities.Update(entity);
            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        public async Task<bool> UpdateRangeAsync(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            _entities.UpdateRange(entity);
            return await Task.FromResult(true).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(params object[] pks)
        {
            if (pks == null) throw new InfrastructureException(nameof(pks), $"El parametro pks no puede ser null");
            TEntity entityToDelete = await _entities.FindAsync(pks).ConfigureAwait(false);
            ValidateNullEntity<TEntity>.IsNullEntity(entityToDelete);

            return DeleteAsync(entityToDelete).Result;
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);

            _entities.Attach(entity);
            _entities.Remove(entity);
            return Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            _entities.Remove((TEntity)entity);
            return await Task.FromResult(true).ConfigureAwait(false);
        }

        #endregion Asynchronous methods

        #region Synchronous methods

        public TEntity Add(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);

            _entities.Add(entity);
            return entity;
        }

        public bool AddRange(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            _entities.AddRangeAsync(entity);
            return true;
        }

        public TEntity Update(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);
            _entities.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _entities.Update(entity);
            return entity;
        }

        public bool UpdateRange(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            _entities.UpdateRange(entity);
            return true;
        }

        public bool Delete(params object[] pks)
        {
            if (pks == null) throw new InfrastructureException(nameof(pks), $"El parametro pks no puede ser null");
            TEntity entityToDelete = _entities.Find(pks);
            ValidateNullEntity<TEntity>.IsNullEntity(entityToDelete);

            return Delete(entityToDelete);
        }

        public bool Delete(TEntity entity)
        {
            ValidateNullEntity<TEntity>.IsNullEntity(entity);

            _entities.Attach(entity);
            _entities.Remove(entity);
            return true;
        }

        public bool DeleteRange(IQueryable<TEntity> entity)
        {
            ValidateNullEntity<TEntity>.IsNullListEntity(entity);

            _entities.Remove((TEntity)entity);
            return true;
        }

        #endregion Synchronous methods

        #endregion Crud methods
    }
}