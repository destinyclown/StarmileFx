using MongoDB.Driver;
using StarmileFx.Models.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace StarmileFx.Common.MongoDB
{
    public interface IRepository<T, in TKey> : IQueryable<T> where T : IEntity<TKey>
    {
        #region Fileds

        /// <summary>
        /// MongoDB表
        /// </summary>
        IMongoCollection<T> DbSet { get; }

        /// <summary>
        /// MongoDB库
        /// </summary>
        IMongoDatabase DbContext { get; }

        #endregion

        #region Find

        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(TKey id);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Insert

        /// <summary>
        /// 插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// 异步插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Adds the new entities in the repository.
        /// </summary>
        /// <param name="entities">The entities of type T.</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// 插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Update

        /// <summary>
        /// 更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        UpdateResult Update(T entity);

        /// <summary>
        /// 异步更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UpdateResult> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Delete

        /// <summary>
        /// 根据主键ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Delete(TKey id);

        /// <summary>
        /// 异步根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        DeleteResult Delete(Expression<Func<T, bool>> predicate);

        #endregion

        #region Other

        /// <summary>
        /// 计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 计数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        #endregion

        #region Query
        /// <summary>
        /// 分页
        /// 注：只适合单属性排序
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        IEnumerable<T> Paged(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> sortBy,
            int pageSize, int pageIndex = 1);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> PagedAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> sortBy,
            int pageSize, int pageIndex = 1,
            CancellationToken cancellationToken = new CancellationToken());

        #endregion
    }


    public interface IRepository<T> : IRepository<T, string>
        where T : IEntity<string>
    {
    }
}
