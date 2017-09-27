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
    public class AuthorizeController : BaseController
    {
        //依赖注入
        private readonly IBaseServer _BaseServer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IBaseServer"></param>
        public AuthorizeController(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        //public IActionResult Index()
        //{
        //    SysRoles model = new SysRoles();
        //    BaseService.Insert(model, HttpContext);
        //    return View();
        //}

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetSysRolesOnline/{Token}")]
        public string GetSysRolesOnline(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = BaseService.GetSysRoleOnline(Token),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 刷新Token令牌
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("RefreshToken/{Token}")]
        public string RefreshToken(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = BaseService.Refresh(Token, HttpContext),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <remarks>
        /// 请求示例:
        ///
        ///     POST /LoginFrom
        ///     {
        ///        "Email": "youremail@qq.com",
        ///        "Password": "******",
        ///        "RememberMe": true,
        ///        "Ip": "192.168.0.1"
        ///     }
        ///
        /// </remarks>
        /// <param name="fromData"></param>
        /// <returns>返回登录授权信息</returns>
        /// <response code="200">返回登录授权信息</response>    
        /// <response code="400">请求报文为空</response>   
        /// <response code="401">未经授权</response>    
        /// <response code="403">禁止访问</response>   
        /// <response code="404">未找到</response>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [ProducesResponseType(typeof(ResponseResult), 400)]
        [ProducesResponseType(typeof(ResponseResult), 401)]
        [ProducesResponseType(typeof(ResponseResult), 403)]
        [ProducesResponseType(typeof(ResponseResult), 404)]
        public IActionResult Login([FromForm]LoginFrom fromData)
        {
            var model = _BaseServer.Login(fromData);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                if (model != null)
                {
                    responseModel.IsSuccess = true;
                    responseModel.Data = BaseService.Insert(model, HttpContext);
                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Data = null;
                    responseModel.Error = new Error { Code = ErrorCode.AuthorizationError, Message = "用户名或密码错误！" };
                }
                return responseModel;
            };
            return Json(funcAction.Invoke()); //ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet("LoadMenuByRole/{Token}")]
        [ValidateParmeterNull("Token")]
        public string LoadMenuByRole(string Token)
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
            return ActionResponseGetString(funcAction);
        }

        ///// <summary>
        ///// 退出登录接口
        ///// </summary>
        ///// <param name="Token"></param>
        ///// <returns></returns>
        //[ValidateParmeterNull("Token")]
        //public string Logout(string Token)
        //{
        //    Func<ResponseResult> funcAction = () =>
        //    {
        //        var responseModel = new ResponseResult();
        //        if (BaseService.ClearRole(Token))
        //        {
        //            responseModel.Data = true;
        //        }
        //        else
        //        {
        //            responseModel.Data = false;
        //        }
        //        return responseModel;
        //    };
        //    return ActionResponseGetString(funcAction);
        //}

        /// <summary>
        /// 处理跨域请求的菜单请求
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetMenuJson/{Token}")]
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
        /// 获取收藏列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetCollectionList/{Token}")]
        public string GetCollectionList(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _BaseServer.GetCollectionList(model),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 处理跨域请求的获取收藏列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        [HttpGet("GetCollectionListJson/{Token}")]
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

        /// <summary>
        /// 收藏菜单
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ConfirmCollection")]
        public string ConfirmCollection([FromForm]WebCollection fromData)
        {
            var model = BaseService.GetRoleByToken(fromData.Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _BaseServer.ConfirmCollection(model, fromData)
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CancelCollection")]
        public string CancelCollection([FromForm]WebCollection fromData)
        {
            var model = BaseService.GetRoleByToken(fromData.Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Data = _BaseServer.CancelCollection(model, fromData)
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
    }
}
