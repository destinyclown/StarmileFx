using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StarmileFx.Api.Server.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        #region 实体处理

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase
        {
            var result = base.Set<TEntity>().FirstOrDefault(whereLambda);
            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            var result = base.Update<TEntity>(entity);
            return result;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            var result = base.Add<TEntity>(entity);
            return result;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        #endregion 实体处理

        #region 批处理

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        public override void AddRange(IEnumerable<object> entities)
        {
            base.AddRange(entities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        public override void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        public override void UpdateRange(IEnumerable<object> entities)
        {
            base.UpdateRange(entities);
        }

        #endregion 批处理

        #region 获取列表

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> whereLambda,
            out int total) where TEntity : ModelBase
        {
            total = this.Set<TEntity>().Count();
            var temp = this.Set<TEntity>().Where(whereLambda).AsNoTracking();
            return temp.AsQueryable();
        }

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<TEntity> List<TEntity>(
            Expression<Func<TEntity, bool>> whereLambda, 
            Func<TEntity, object> orderbyLambda,
            out int total) where TEntity : ModelBase
        {
            total = this.Set<TEntity>().Count();
            var temp = this.Set<TEntity>().Where(whereLambda).AsNoTracking()
                .OrderBy<TEntity, object>(orderbyLambda);
            return temp.AsQueryable();
        }

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="TEntity">泛型</typeparam>  
        /// <param name="pageData">分页实体</param>
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="total">总数量</param>  
        /// <returns>IQueryable 泛型集合</returns> 
        public IQueryable<TEntity> PageData<TEntity>(
            PageData PageData,
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda,
            out int total
            ) where TEntity : ModelBase
        {
            total = this.Set<TEntity>().Where(whereLambda).Count();
            if (PageData.IsAsc)
            {
                var temp = this.Set<TEntity>().Where(whereLambda)
                             .OrderBy<TEntity, object>(orderbyLambda)
                             .Skip(PageData.PageSize * (PageData.PageIndex - 1))
                             .Take(PageData.PageSize);
                return temp.AsQueryable();
            }
            else
            {
                var temp = this.Set<TEntity>().Where(whereLambda)
                           .OrderByDescending<TEntity, object>(orderbyLambda)
                           .Skip(PageData.PageSize * (PageData.PageIndex - 1))
                           .Take(PageData.PageSize);
                return temp.AsQueryable();
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <typeparam name="TEntity">泛型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IQueryable<TEntity> ExecuteSql<TEntity>(string sql, params object[] parameters) where TEntity : ModelBase
        {
            return Set<TEntity>().FromSql(sql, parameters);
        }


        public IQueryable<TEntity> ExecuteSqlProcedure<TEntity>(string procedureName, params object[] parameters) where TEntity : ModelBase
        {
            string sql = "exec" + procedureName; 
            return ExecuteSql<TEntity>(sql, parameters);
        }

        #endregion 获取列表

        public DbSet<SysAuthorities> SysAuthorities { get; set; }
        public DbSet<SysEmailLogs> SysEmailLogs { get; set; }
        public DbSet<SysMessage> SysLogs { get; set; }
        public DbSet<SysRoleLogs> SysRoleLogs { get; set; }
        public DbSet<SysRolePermissions> SysRolePermissions { get; set; }
        public DbSet<SysRoles> SysRoles { get; set; }
    }
}
