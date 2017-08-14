using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Api.Services;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using static StarmileFx.Models.Web.HomeFromModel;
using StarmileFx.Api.FilterAttributes;

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
            //return "启动StarmileFx.Api系统！";
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
                var responseModel = new ResponseResult();
                responseModel.Content = BaseService.GetSysRoleOnline(Token);
                responseModel.IsSuccess = true;
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
                var responseModel = new ResponseResult();
                responseModel.Content = BaseService.Refresh(Token, HttpContext);
                responseModel.IsSuccess = true;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <param name="IP"></param>
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
                var responseModel = new ResponseResult();
                responseModel.Content = _BaseServer.LoadMenuByRole(model);
                responseModel.IsSuccess = true;
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
    }
}
