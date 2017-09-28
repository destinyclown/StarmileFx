using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Models.Web;
using StarmileFx.Api.Services;
using StarmileFx.Models;
using static StarmileFx.Models.Enum.BaseEnum;
using StarmileFx.Api.FilterAttributes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Api.Controllers
{
    /// <summary>
    /// 基本控制器
    /// </summary>
    [Route("api/[controller]")]
    public class BaseController : ApiController
    {
        //依赖注入
        private readonly IBaseServer _BaseServer;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="IBaseServer"></param>
        public BaseController(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        /// <response code="200">返回获取在线用户列表</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [ValidateParmeterNull("Token")]
        [HttpGet("GetSysRolesOnline")]
        public IActionResult GetSysRolesOnline(string Token)
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
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        /// <response code="200">返回刷新令牌信息</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [ValidateParmeterNull("Token")]
        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken(string Token)
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

        #region 登录
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
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [HttpPost("Login")]
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
            return ActionResponseGetString(funcAction); //ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 退出登录接口
        /// </summary>
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        /// <response code="200">退出登录信息</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [HttpGet("Logout")]  
        [ValidateParmeterNull("Token")]
        public IActionResult Logout(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                if (BaseService.ClearRole(Token))
                {
                    responseModel.Data = true;
                }
                else
                {
                    responseModel.Data = false;
                }
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        /// <response code="200">返回菜单列表</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [HttpGet("LoadMenuByRole")]
        [ValidateParmeterNull("Token")]
        public IActionResult LoadMenuByRole(string Token)
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

        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="Token">Token令牌</param>
        /// <returns></returns>
        /// <response code="200">返回收藏列表</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [ValidateParmeterNull("Token")]
        [HttpGet("GetCollectionList")]
        public IActionResult GetCollectionList(string Token)
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
        /// 收藏菜单
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        /// <response code="200">返回收藏信息</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [HttpPost]
        [Route("ConfirmCollection")]
        public IActionResult ConfirmCollection([FromForm]WebCollection fromData)
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
        /// <response code="200">返回取消收藏信息</response>
        [ProducesResponseType(typeof(ResponseResult), 200)]
        [HttpPost]
        [Route("CancelCollection")]
        public IActionResult CancelCollection([FromForm]WebCollection fromData)
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
        #endregion
    }
}
