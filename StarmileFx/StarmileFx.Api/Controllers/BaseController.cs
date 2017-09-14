using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Services;
using StarmileFx.Common;
using StarmileFx.Models;
using StarmileFx.Api.FilterAttributes;
using static StarmileFx.Models.Enum.BaseEnum;
using Microsoft.AspNetCore.Mvc.Filters;
using StarmileFx.Models.Base;
using StarmileFx.Api.Server.IServices;

namespace StarmileFx.Api.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    //[TypeFilter(typeof(OverallExceptionFilterAttribute))]
    public class BaseController : Controller
    {
        public BaseController() { }

        //依赖注入
        private readonly IBaseServer _BaseServer;

        public BaseController(IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //认证暂时不使用
            string Authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
        }

        /// <summary>
        /// 返回结果json
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string ActionResponseGetString(Func<ResponseResult> action)
        {
            return ActionResponse(action);
        }

        /// <summary>
        /// 处理跨域请求
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string ActionResponseJsonp(Func<ResponseResult> action)
        {
            string callback = Request.Query["callback"];
            return string.Format("{0}({1})", callback, ActionResponse(action));
        }

        /// <summary>
        /// 执行请求内容，返回结果json
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string ActionResponse(Func<ResponseResult> action)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                DateTime timer = DateTime.Now;
                result = action.Invoke();
                //创建日志记录
                SysLog log = new SysLog
                {
                    Ip = GetUserIp(),
                    IsError = false,
                    Herf = Request.Path.Value,
                    ResponseSpan = (decimal)DateTime.Now.Subtract(timer).Milliseconds / 1000
                };
                //_BaseServer.Logger(log);
                return JsonHelper.T_To_Json(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Data = null;
                result.Error = new Error
                {
                    Code = ex.GetType().ToString() == "System.Exception" ? ErrorCode.DataError : ErrorCode.SystemError,
                    Message = ex.Message
                };
                result.SystemError = ex.GetType().ToString() == "System.Exception" ? null : new SystemError
                {
                    ExceptionType = ex.GetType().ToString(),
                    Message = ex.Message,
                    HelpUrl = "https://api.starmile.com.cn/api/help/" + ErrorCode.SystemError,
                };
                return JsonHelper.T_To_Json(result);
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public string GetUserIp()
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 发送错误邮件
        /// </summary>
        /// <param name="Controller"></param>
        /// <param name="Action"></param>
        /// <param name="ErrorMsg"></param>
        private void SendErrorEmail(string Controller, string Action, string ErrorMsg)
        {
            Email email = new Email
            {
                Message = string.Format("StarmileFx.Api系统出错\r\n客户端IP地址：{0}\r\nController：{1}\r\nAction：{2}\r\n错误信息：{3}", GetUserIp(), Controller, Action, ErrorMsg),
                Subject = "StarmileFx.Api系统出错",
                type = EmailTypeEnum.Error
            };
            EmailService.Add(email);
        }
    }
}
