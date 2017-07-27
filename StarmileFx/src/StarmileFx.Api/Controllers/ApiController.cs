﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Server.IServices;
using StarmileFx.Api.Services;
using StarmileFx.Models;
using StarmileFx.Models.Base;
using static StarmileFx.Models.Web.HomeFromModel;

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

        public string CreateSystem()
        {
            SysRoles model = new SysRoles();
            Email email = new Email();
            email.Message = string.Format("用户于{0}启动StarmileFx.Api系统，请持续跟踪系统邮件！", DateTime.Now);
            email.Subject = "启动StarmileFx.Api系统";
            email.type = StarmileFx.Models.Enum.BaseEnum.EmailTypeEnum.Error;
            EmailService.Add(email);
            BaseService.Insert(model, HttpContext);
            return "启动StarmileFx.Api系统！";
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpPost]
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
        public string LoginAsync([FromForm]LoginFrom fromData)
        {
            var model = _BaseServer.LoginAsync(fromData);
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
        public string LoadMenuByRoleAsync(string Token)
        {
            var model = BaseService.GetRoleByToken(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _BaseServer.LoadMenuByRoleAsync(model);
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
        [HttpPost]
        public string LogoutAsync(string Token)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                Result result = new Result();
                if (Token != null && BaseService.ClearRole(Token))
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

        public string GetSysRoleLogsList([FromForm]PageData page)
        {
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult();
                responseModel.Content = _BaseServer.GetSysRoleLogsList(page);
                responseModel.IsSuccess = true;
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
    }
}
