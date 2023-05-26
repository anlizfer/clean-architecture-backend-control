using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeFirst.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Query methods

        #region Asynchronous methods

        /// <summary>
        /// Gets all the elements of an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <param name="skip">
        /// The method that takes the first n elements of the n amount Taken by the Take method.
        /// </param>
        /// <param name="take">
        /// The method that extracts the first n elements
        /// where it returns a new sequence containing only the n elements taken.
        /// </param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null
            );

        /// <summary>
        /// get all the element of a paged entity.
        /// </summary>
        /// <typeparam name="k"></typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageCount">Count of the page.</param>
        /// <param name="orderByExpression">The type of the order by.</param>
        /// <param name="ascending"></param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetPagedElementsAsync<k>(
            int pageIndex,
            int pageCount,
            Expression<Func<TEntity, k>> orderByExpression,
            bool ascending = true,
            string includeProperties = ""
            );

        /// <summary>
        /// Gets all the elements of an entity filter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <param name="skip">The method that takes the first n elements of the n amount Taken by the Take method.</param>
        /// <param name="take">The method that extracts the first n elements where it returns a new sequence containing only the n elements taken.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null);

        /// <summary>
        /// Get data one entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <returns></returns>
        Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> filter = null,
        string includeProperties = null);

        /// <summary>
        ///  Get data one entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <returns></returns>
        Task<TEntity> GetFirstAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null);

        /// <summary>
        ///  Get true or false entity if exist.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <returns></returns>
        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Get count of entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Get a entity for Id
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The key value.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(params object[] pks);

        /// <summary>
        /// Stored procedure SQL
        /// </summary>
        /// <param name="query">Stored procedure or sql statement to execute</param>
        /// <param name="parameters">Parameters or multiple parameters required by the stored procedure</param>
        /// <returns>Returns a list of objects</returns>
        Task<IEnumerable<TEntity>> ExecStoreProcedureAsync(string query, params object[] parameters);

        /// <summary>
        /// Stored procedure SQL
        /// </summary>
        /// <param name="query">Stored procedure or sql statement to execute</param>
        /// <returns>Returns a list of objects</returns>
        Task<IEnumerable<TEntity>> ExecStoreProcedureAsync(string query);

        #endregion Asynchronous methods

        #region Synchronous methods

        /// <summary>
        /// Get all th date of a entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <param name="skip">The method that takes the first n elements of the n amount Taken by the Take method.</param>
        /// <param name="take">The method that extracts the first n elements where it returns a new sequence containing only the n elements taken.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null);

        /// <summary>
        /// Get all data of entity Filter
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <param name="skip">The method that takes the first n elements of the n amount Taken by the Take method.</param>
        /// <param name="take">The method that extracts the first n elements where it returns a new sequence containing only the n elements taken.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Get One entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <returns></returns>
        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);

        /// <summary>
        /// Get one entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <param name="orderBy">The type of the order by.</param>
        /// <param name="includeProperties">Multiple Levels.</param>
        /// <returns></returns>
        TEntity GetFirst(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);

        /// <summary>
        /// Get one entity for id
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The key value.</param>
        /// <returns></returns>
        TEntity GetById(params object[] pks);

        /// <summary>
        /// Get count of the list entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <returns></returns>
        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Get if exist a entity
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="filter">The predicate.</param>
        /// <returns></returns>
        bool GetExists(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Stored procedure SQL
        /// </summary>
        /// <param name="query">Stored procedure or sql statement to execute</param>
        /// <param name="parameters">Parameters or multiple parameters required by the stored procedure</param>
        /// <returns>Returns a list of objects</returns>
        IEnumerable<TEntity> ExecStoreProcedure(string query, params object[] parameters);

        /// <summary>
        /// Stored procedure SQL
        /// </summary>
        /// <param name="query">Stored procedure or sql statement to execute</param>
        /// <returns>Returns a list of objects</returns>
        IEnumerable<TEntity> ExecStoreProcedure(string query);

        #endregion Synchronous methods

        #endregion Query methods

        #region Crud methods

        #region Asynchronous methods

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// add rangue of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddRangeAsync(IQueryable<TEntity> entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update rangue of th entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(IQueryable<TEntity> entity);

        /// <summary>
        /// Dlete a entity for id
        /// </summary>
        /// <param name="id">The key value.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(params object[] pks);

        /// <summary>
        /// Delete a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete a rangue of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteRangeAsync(IQueryable<TEntity> entity);

        #endregion Asynchronous methods

        #region Synchronous methods

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Add rangue of entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddRange(IQueryable<TEntity> entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Update rangue entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateRange(IQueryable<TEntity> entity);

        /// <summary>
        /// Delete a entity for id
        /// </summary>
        /// <param name="id">The key value.</param>
        /// <returns></returns>
        bool Delete(params object[] pks);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(TEntity entity);

        /// <summary>
        /// Delete rangue entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteRange(IQueryable<TEntity> entity);

        #endregion Synchronous methods

        #endregion Crud methods
    }
}