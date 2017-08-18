using StarmileFx.Common.Enum;
using StarmileFx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Net;
using StarmileFx.Models.Json;
using Microsoft.Extensions.Options;

namespace StarmileFx.Web.Filter
{
    /// <summary>
    /// 用于网站维护
    /// </summary>
    public class ActionRestrict : ActionFilterAttribute
    {
        private readonly IOptions<WebConfig> _WebConfig;
        public ActionRestrict(IOptions<WebConfig> WebConfig)
        {
            _WebConfig = WebConfig;
        }
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
            //判断网站是否维护
            if (!_WebConfig.Value.WebState)
            {
                ContentResult cr = new ContentResult
                {
                    Content = "<div><script>top.location.href='/home/maintain';</script></div>",
                    StatusCode = (int)HttpStatusCode.ServiceUnavailable
                };
                filterContext.Result = cr;
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
