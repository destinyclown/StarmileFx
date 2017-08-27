using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Api.Services;
using StarmileFx.Common;
using StarmileFx.Models;

namespace StarmileFx.Api.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public string ActionResponseGetString(Func<ResponseResult> action)
        {
            ResponseResult result = ActionResponse(action);
            result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
            try
            {
                result.IsSuccess = true;
                string json = JsonHelper.T_To_Json(result);
                return json;
            }
            catch (Exception ex)
            {
                result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
                string json = JsonHelper.T_To_Json(result);
                SendErrorEmail(RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString(), ex.Message);
                LogHelper.Error(result);
                return json;
            }
        }

        public ResponseResult ActionResponse(Func<ResponseResult> action)
        {
            ResponseResult result = new ResponseResult
            {
                FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString()
            };
            try
            {
                result = action.Invoke();
                result.SendDateTime = DateTime.Now;
                if (!result.IsSuccess && !string.IsNullOrEmpty(result.ErrorMsg))
                {
                    result.ErrorMsg = result.ErrorMsg;
                    SendErrorEmail(RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString(), result.ErrorMsg);
                    LogHelper.Error(result);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
                SendErrorEmail(RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString(), ex.Message);
                LogHelper.Error(result);
            }

            return result;
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
                type = StarmileFx.Models.Enum.BaseEnum.EmailTypeEnum.Error
            };
            EmailService.Add(email);
        }
    }
}
