using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Models.Json;
using Microsoft.AspNetCore.Http;
using static StarmileFx.Models.Web.HomeFromModel;

namespace StarmileFx.Web.Server.IServices
{
    /// <summary>
    /// DateBase数据库接口
    /// </summary>
    public interface IBaseServer
    {
        Task<ResponseResult<bool>> RefreshToken(string Token);

        Task<ResponseResult<Result>> Logout(string Token);

        Task<ResponseResult<Result>> Login(LoginFrom fromData);

        Task<ResponseResult<SysMenusModel>> LoadMenuByRole(string Token);

        Task<ResponseResult<List<SysRoleLogs>>> GetSysRoleLogsList(PageData page);
    }
}
