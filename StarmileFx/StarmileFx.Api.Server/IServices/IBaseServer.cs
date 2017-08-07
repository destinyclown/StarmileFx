using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using static StarmileFx.Models.Web.HomeFromModel;

namespace StarmileFx.Api.Server.IServices
{
    /// <summary>
    /// DateBase数据库接口
    /// </summary>
    public interface IBaseServer
    {
        #region 基本操作

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase;

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        bool Update<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase;

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        bool Add<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        bool AddRange(IEnumerable<object> entities);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        bool UpdateRange(IEnumerable<object> entities);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        bool RemoveRange(IEnumerable<object> entities);

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> List<TEntity>(Expression<Func<TEntity, bool>> whereLambda,
            out int total) where TEntity : ModelBase;

        /// <summary>
        /// 获取整个列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="orderbyLambda"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        IQueryable<TEntity> List<TEntity>(
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda,
            out int total) where TEntity : ModelBase;

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="TEntity">泛型</typeparam>  
        /// <param name="pageData">分页实体</param>
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="total">总数量</param>  
        /// <returns>IQueryable 泛型集合</returns> 
        IQueryable<TEntity> PageData<TEntity>(
            PageData pageData,
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda,
            out int total
            ) where TEntity : ModelBase;

        #endregion 基本操作

        #region 登录主页操作
        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <param name="IP"></param>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        SysRoles LoginAsync(LoginFrom fromData);

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="rel"></param>
        /// <returns></returns>
        //Task<Result> LoginAsync(string LoginName, string Password, string IP, out SysRoles sysRole);

        //Task<List<SysMenus>> LoadMenuList(string roleid);

        //Task<List<SysViews>> LoadViewList(string roleid, Guid menuid);

        SysMenusModel LoadMenuByRoleAsync(SysRoles role);

        List<SysRoleLogs> GetSysRoleLogsList(PageData page);
        #endregion
    }
}
