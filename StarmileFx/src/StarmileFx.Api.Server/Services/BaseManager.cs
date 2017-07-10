using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StarmileFx.Api.Server.Data;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using static StarmileFx.Models.Web.HomeFromModel;
using static StarmileFx.Models.Json.SysMenusModel;
using System.Collections;

namespace StarmileFx.Api.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class BaseManager : IBaseServer
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        private BaseContext _DataContext;
        private IOptions<SysMenusModel> _SysMenusModel;

        public BaseManager(BaseContext DataContext, IOptions<SysMenusModel> SysMenusModel)
        {
            _DataContext = DataContext;
            _SysMenusModel = SysMenusModel;
        }

        #region 基本数据库处理
        #region 实体处理

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        private bool Commit()
        {
            return _DataContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Lambda获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public TEntity Get<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : ModelBase
        {
            return _DataContext.Get<TEntity>(whereLambda);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase
        {
            _DataContext.Update<TEntity>(entity);
            if (IsCommit)
            {
                return Commit();
            }
            return false;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsCommit">是否提交</param>
        /// <returns></returns>
        public bool Add<TEntity>(TEntity entity, bool IsCommit = true) where TEntity : ModelBase
        {
            _DataContext.Add<TEntity>(entity);
            if (IsCommit)
            {
                return Commit();
            }
            return false;
        }

        #endregion 实体处理

        #region 批处理

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool AddRange(IEnumerable<object> entities)
        {
            _DataContext.AddRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool UpdateRange(IEnumerable<object> entities)
        {
            _DataContext.UpdateRange(entities);
            return Commit();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool RemoveRange(IEnumerable<object> entities)
        {
            _DataContext.RemoveRange(entities);
            return Commit();
        }

        #endregion

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
            return _DataContext.List<TEntity>(whereLambda, out total);
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
            return _DataContext.List<TEntity>(whereLambda, orderbyLambda, out total);
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
            PageData pageData, 
            Expression<Func<TEntity, bool>> whereLambda,
            Func<TEntity, object> orderbyLambda, 
            out int total
            ) where TEntity : ModelBase
        {
            return _DataContext.PageData<TEntity>(pageData, whereLambda, orderbyLambda, out total);
        }
        #endregion 获取列表
        #endregion

        #region HomeController
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="rel"></param>
        /// <returns></returns>
        public SysRoles LoginAsync(LoginFrom fromData)
        {
            SysRoles sysRole = new SysRoles();
            sysRole = Get<SysRoles>(a => a.LoginName == fromData.loginName && a.Pwd == fromData.password);
            if (sysRole != null)
            {
                SysRoleLogs logs = new SysRoleLogs()
                {
                    LoginIP = fromData.ip,
                    RoleID = sysRole.ID
                };
                Add(logs);
            }
            return sysRole;
        }

        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SysMenusModel LoadMenuByRoleAsync(SysRoles role)
        {
            var _SysMenusModelList = _SysMenusModel.Value;
            if (role != null)
            {
                if (role.Permissions == 0)
                    return _SysMenusModelList;
                else
                {
                    var permissionsId = Get<SysRolePermissions>(a => a.Permissions == role.Permissions && a.State).ID;
                    int total = 0;
                    var list = (from a in List<SysAuthorities>(a => a.PermissionsID == permissionsId && a.State, out total)
                                select new string(a.Code.ToCharArray())).ToList();
                    var mainMenuList = (_SysMenusModelList.MainMenuList
                        .Where(a => list.Contains(a.Code) && a.State)).ToList();
                    for (int i = 0; i < mainMenuList.Count; i++)
                    {
                        mainMenuList[i].MainMenuBase = mainMenuList[i].MainMenuBase
                            .Where(a => list.Contains(a.Code) && a.State).ToList();
                        for (int j = 0; j < mainMenuList[i].MainMenuBase.Count; j++)
                        {
                            mainMenuList[i].MainMenuBase[j].MenuList = mainMenuList[i].MainMenuBase[j].MenuList
                                .Where(a => list.Contains(a.Code) && a.State).ToList();
                        }
                    }
                    return new SysMenusModel() { MainMenuList = mainMenuList };

                }
            }
            return null;
        }
        #endregion home
    }
}
