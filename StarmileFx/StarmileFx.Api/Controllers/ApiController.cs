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
    [Produces("application/json")]
    public class ApiController : Controller
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ApiController() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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
        protected IActionResult ActionResponseGetString(Func<ResponseResult> action)
        {
            return Json(ActionResponse(action));
        }

        /// <summary>
        /// 处理跨域请求
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IActionResult ActionResponseJsonp(Func<ResponseResult> action)
        {
            string callback = Request.Query["callback"];
            return Content(string.Format("{0}({1})", callback, JsonHelper.T_To_Json(ActionResponse(action))));
        }

        /// <summary>
        /// 执行请求内容，返回结果
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ResponseResult ActionResponse(Func<ResponseResult> action)
        {
            ResponseResult result = new ResponseResult();
            DateTime timer = DateTime.Now;
            try
            {
                result = action.Invoke();
                //创建日志记录
                SysLog log = new SysLog
                {
                    Ip = GetUserIp(),
                    IsError = false,
                    Herf = Request.Path.Value,
                    ResponseSpan = (decimal)DateTime.Now.Subtract(timer).Milliseconds / 1000
                };
                LogService.Add(log);
                return result;
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
                //创建日志记录
                SysLog log = new SysLog
                {
                    Ip = GetUserIp(),
                    Code = result.Error.Code,
                    ErrorMessage = ex.Message,
                    IsError = true,
                    Herf = Request.Path.Value,
                    ResponseSpan = (decimal)DateTime.Now.Subtract(timer).Milliseconds / 1000
                };
                LogService.Add(log);
                return result;
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        protected string GetUserIp()
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
        protected void SendErrorEmail(string Controller, string Action, string ErrorMsg)
        {
            Email email = new Email
            {
                Message = string.Format("StarmileFx.Api系统出错\r\n客户端IP地址：{0}\r\nController：{1}\r\nAction：{2}\r\n错误信息：{3}", GetUserIp(), Controller, Action, ErrorMsg),
                Subject = "StarmileFx.Api系统出错",
                Type = EmailTypeEnum.Error
            };
            EmailService.Add(email);
        }
    }
}
