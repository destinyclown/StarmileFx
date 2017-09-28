using System;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Api.Services;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using StarmileFx.Api.FilterAttributes;
using StarmileFx.Models.Web;
using static StarmileFx.Models.Enum.BaseEnum;

namespace StarmileFx.Api.Controllers
{
    /// <summary>
    /// 授权控制类
    /// </summary>
    [Route("api/[controller]")]
    public class AuthorizeController : ApiController
    {
        //依赖注入
        private readonly IBaseServer _BaseServer;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="IBaseServer"></param>
        public AuthorizeController(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        /// <summary>
        /// 处理跨域请求的菜单请求
        /// </summary>
        /// <param name="Token" required="true">Token令牌</param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetMenuJson")]
        public string GetMenuJson(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _BaseServer.LoadMenuByRole(model),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseJsonp(funcAction);
        }

        /// <summary>
        /// 处理跨域请求的获取收藏列表
        /// </summary>
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetCollectionListJson")]
        public string GetCollectionListJson(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = new
                    {
                        UserId = model.LoginName,
                        MenuList = _BaseServer.GetCollectionList(model)
                    },
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseJsonp(funcAction);
        }
    }
}
