using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using Microsoft.Extensions.Options;
using static StarmileFx.Models.Web.HomeFromModel;
using SqlSugar;
using StarmileFx.Api.Server.BaseData;

namespace StarmileFx.Api.Server.Services
{
    /// <summary>
    /// 接口实现
    /// </summary>
    public class BaseManager : IBaseServer
    {
        
        private SqlSugarClient _db;
        private IOptions<ConnectionStrings> _ConnectionStrings;

        public BaseManager(IOptions<ConnectionStrings> ConnectionStrings)
        {
            _ConnectionStrings = ConnectionStrings;
            _db = BaseClient.GetInstance(_ConnectionStrings.Value.BaseConnection);
        }

        #region HomeController
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="rel"></param>
        /// <returns></returns>
        public SysRoles Login(LoginFrom fromData)
        {
            SysRoles sysRole = new SysRoles();
            sysRole = _db.Queryable<SysRoles>().First(a => a.LoginName == fromData.loginName && a.Pwd == fromData.password);
            if (sysRole != null)
            {
                SysRoleLogs logs = new SysRoleLogs()
                {
                    LoginIP = fromData.ip,
                    RoleID = sysRole.Id
                };
                _db.Insertable(logs).ExecuteCommand();
            }
            return sysRole;
        }

        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysMenus> LoadMenuByRole(SysRoles role)
        {
            var _SysMenuslList = _db.Queryable<SysMenus>().Where(a => a.State).ToList();
            if (role != null)
            {
                if (role.Permissions == 0)
                {
                    return _SysMenuslList;
                }
                else
                {
                    var list = _db.Queryable<SysRolePermissions, SysAuthorities>((srp, sa) => 
                    new object[] {
                        JoinType.Left,srp.Id == sa.PermissionsID && srp.Permissions == role.Permissions && srp.State && sa.State
                    }).Select((srp, sa) => 
                    new string(sa.Code.ToCharArray())).ToList();

                    var mainMenuList = (_SysMenuslList
                        .Where(a => list.Contains(a.Code))).ToList();

                    return mainMenuList;
                }
            }
            return null;
        }
        #endregion home
    }
}
