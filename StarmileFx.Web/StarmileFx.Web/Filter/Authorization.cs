using StarmileFx.Common.Enum;
using StarmileFx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace StarmileFx.Web.Filter
{
    public class Authorization : ActionFilterAttribute
    {
        private RouteValueDictionary loginRoute = new RouteValueDictionary(new { controller = "home", action = "login" });

        /// <summary>
        /// 登录的路由
        /// </summary>
        public RouteValueDictionary LoginRoute
        {
            get { return loginRoute; }
            set { loginRoute = value; }
        }

        private CallType callType = CallType.Normal;
        /// <summary>
        /// 调用类型
        /// </summary>
        public CallType _CallType
        {
            get { return callType; }
            set { callType = value; }
        }

        /// <summary>
        /// action执行前判断
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string session = filterContext.HttpContext.Session.GetString(SysConst.Token);
            
            if (string.IsNullOrEmpty(session))
            {
                if (callType == CallType.Normal)
                {
                    filterContext.Result = new RedirectToRouteResult(loginRoute);
                }
                else if (callType == CallType.AjaxCall)
                {
                    ContentResult jsonData = new ContentResult();
                    Result result = new Result();
                    result.Reason = HttpStatusCode.OK;
                    filterContext.Result = jsonData;
                }
                else if (callType == CallType.Partial)
                {
                    ContentResult cr = new ContentResult();
                    cr.Content = "<div><script>top.location.href='/home/login';</script></div>";
                    filterContext.Result = cr;
                }
            }
        }
        /// <summary>
        /// 调用类型
        /// </summary>
        public enum CallType
        {
            /// <summary>
            /// 普通请求(包括)
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 无刷新请求
            /// </summary>
            AjaxCall = 1,

            /// <summary>
            /// 分部视图请求
            /// </summary>
            Partial = 2
        }
    }
}
