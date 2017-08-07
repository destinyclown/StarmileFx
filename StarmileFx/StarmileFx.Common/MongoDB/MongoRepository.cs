using MongoDB.Bson;
using MongoDB.Driver;
using StarmileFx.Models.MongoDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace StarmileFx.Common.MongoDB
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity<string>
    {
        #region Constructor

        protected MongoRepository(IMongoCollection<T> collection)
        {
            DbSet = collection;
            DbContext = collection.Database;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return DbSet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region 字段

        public Type ElementType => DbSet.AsQueryable().ElementType;
        public Expression Expression => DbSet.AsQueryable().Expression;
        public IQueryProvider Provider => DbSet.AsQueryable().Provider;
        public IMongoCollection<T> DbSet { get; }
        public IMongoDatabase DbContext { get; }

        #endregion

        #region Find

        public T GetById(string id)
        {
            return Get(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FindSync(predicate).Current;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var task = await DbSet.FindAsync(predicate, null, cancellationToken);
            return task.Current;
        }

        #endregion

        #region Insert

        public T Insert(T entity)
        {
            DbSet.InsertOne(entity);
            return entity;
        }

        public Task InsertAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return DbSet.InsertOneAsync(entity, null, cancellationToken);
        }

        public void Insert(IEnumerable<T> entities)
        {
            DbSet.InsertMany(entities);
        }

        public Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new CancellationToken())
        {
            return DbSet.InsertManyAsync(entities, null, cancellationToken);
        }

        #endregion

        #region Update

        public UpdateResult Update(T entity)
        {
            var doc = entity.ToBsonDocument();
            return DbSet.UpdateOne(Builders<T>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<T>(doc));
        }

        public Task<UpdateResult> UpdateAsync(T entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var doc = entity.ToBsonDocument();
            return DbSet.UpdateOneAsync(Builders<T>.Filter.Eq(e => e.Id, entity.Id),
                new BsonDocumentUpdateDefinition<T>(doc), cancellationToken: cancellationToken);
        }

        #endregion

        #region Delete

        public T Delete(string id)
        {
            return DbSet.FindOneAndDelete(a => a.Id.Equals(id));
        }

        public Task<T> DeleteAsync(string id, CancellationToken cancellationToken = new CancellationToken())
        {
            return DbSet.FindOneAndDeleteAsync(a => a.Id.Equals(id), null, cancellationToken);
        }

        public Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return DbSet.DeleteManyAsync(predicate, cancellationToken);
        }

        public DeleteResult Delete(Expression<Func<T, bool>> predicate)
        {
            return DbSet.DeleteMany(predicate);
        }

        #endregion

        #region Other

        public long Count(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return DbSet.CountAsync(predicate, null, cancellationToken);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Get(predicate).Any();
        }

        #endregion

        #region Page

        public IEnumerable<T> Paged(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> sortBy,
            int pageSize, int pageIndex = 1)
        {
            var sort = Builders<T>.Sort.Descending(sortBy);
            return DbSet.Find(predicate).Sort(sort).Skip(pageSize * pageIndex - 1).Limit(pageSize).ToList();
        }

        public Task<List<T>> PagedAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> sortBy,
            int pageSize, int pageIndex = 1,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.Run(() =>
            {
                var sort = Builders<T>.Sort.Descending(sortBy);
                return DbSet.Find(predicate).Sort(sort).Skip(pageSize * pageIndex - 1).Limit(pageSize).ToList();
            }, cancellationToken);
        }

        #endregion

        #region Helper

        /// <summary>
        /// 获取类型的所有属性信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="select"></param>
        /// <returns></returns>
        private PropertyInfo[] GetPropertyInfos<TProperty>(Expression<Func<T, TProperty>> select)
        {
            var body = select.Body;
            switch (body.NodeType)
            {
                case ExpressionType.Parameter:
                    var parameterExpression = body as ParameterExpression;
                    if (parameterExpression != null) return parameterExpression.Type.GetProperties();
                    break;
                case ExpressionType.New:
                    var newExpression = body as NewExpression;
                    if (newExpression != null)
                        return newExpression.Members.Select(m => m as PropertyInfo).ToArray();
                    break;
            }
            return null;
        }

        #endregion
    }
}
