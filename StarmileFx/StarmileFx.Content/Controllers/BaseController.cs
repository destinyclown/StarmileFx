using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Common;
using StarmileFx.Models;
using Microsoft.AspNetCore.Authorization;

namespace StarmileFx.Content.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController() { }

        /// <summary>
        /// 返回结果json
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
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
                LogHelper.Error(result);
                return json;
            }
        }

        /// <summary>
        /// 处理跨域请求
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string ActionResponseJsonp(Func<ResponseResult> action)
        {
            ResponseResult result = ActionResponse(action);
            string callback = Request.Query["callback"];
            result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
            try
            {
                result.IsSuccess = true;
                string json = string.Format("{0}({1})", callback, JsonHelper.T_To_Json(result));
                return json;
            }
            catch (Exception ex)
            {
                result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
                string json = string.Format("{0}({1})", callback, JsonHelper.T_To_Json(result));
                LogHelper.Error(result);
                return json;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
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
                    LogHelper.Error(result);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
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
    }
}
