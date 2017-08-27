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
        #region 登录主页操作
        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <param name="IP"></param>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        SysRoles Login(LoginFrom fromData);

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

        List<SysMenus> LoadMenuByRole(SysRoles role);
        #endregion
    }
}
