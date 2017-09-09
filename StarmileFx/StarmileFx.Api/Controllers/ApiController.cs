using System;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Api.Services;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using static StarmileFx.Models.Web.HomeFromModel;
using StarmileFx.Api.FilterAttributes;
using StarmileFx.Models.Web;

namespace StarmileFx.Api.Controllers
{
    public class ApiController : BaseController
    {
        //依赖注入
        private readonly IBaseServer _BaseServer;

        public ApiController(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        public IActionResult Index()
        {
            SysRoles model = new SysRoles();
            BaseService.Insert(model, HttpContext);
            return View();
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        public string GetSysRolesOnline(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = BaseService.GetSysRoleOnline(Token),
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
        public string RefreshToken(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = BaseService.Refresh(Token, HttpContext),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        public string Login([FromForm]LoginFrom fromData)
        {
            var model = _BaseServer.Login(fromData);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                Result result = new Result();
                if (model != null)
                {
                    responseModel.Token = BaseService.Insert(model, HttpContext);
                    result.IsSuccessful = true;
                    result.ReasonDescription = "登录成功！";
                }
                else
                {
                    result.ReasonDescription = "用户名或密码错误！";
                }
                responseModel.Content = result;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 获取系统菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        public string LoadMenuByRole(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = _BaseServer.LoadMenuByRole(model),
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 退出登录接口
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        public string Logout(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                Result result = new Result();
                if (BaseService.ClearRole(Token))
                {
                    result.IsSuccessful = true;
                    result.ReasonDescription = "登录成功！";
                }
                else
                {
                    result.ReasonDescription = "Token错误，请检查！";
                }
                responseModel.Content = result;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 处理跨域请求的菜单请求
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [ValidateParmeterNull("Token")]
        public string GetMenuJson(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = _BaseServer.LoadMenuByRole(model),
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
        public string GetCollectionList(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = _BaseServer.GetCollectionList(model),
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
        public string GetCollectionListJson(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = new
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
        public string ConfirmCollection([FromForm]WebCollection fromData)
        {
            var model = BaseService.GetRoleByToken(fromData.Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                Result result = new Result
                {
                    IsSuccessful = _BaseServer.ConfirmCollection(model, fromData)
                };
                responseModel.Content = result;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        public string CancelCollection([FromForm]WebCollection fromData)
        {
            var model = BaseService.GetRoleByToken(fromData.Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                Result result = new Result
                {
                    IsSuccessful = _BaseServer.CancelCollection(model, fromData)
                };
                responseModel.Content = result;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
    }
}
