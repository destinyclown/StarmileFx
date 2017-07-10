using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using StarmileFx.Web.Controllers.Controllers;
using StarmileFx.Server.IServices;
using StarmileFx.Models;

namespace StarmileFx.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 返回结果对象
        /// </summary>
        public Result result = new Result();

        /// <summary>
        /// Token令牌
        /// </summary>
        public string token { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            result = new Result();
            token = HttpContext.Session.GetString(SysConst.Token); 
            base.OnActionExecuting(context);
            string session = context.HttpContext.Session.GetString(SysConst.Token);
            string action = context.RouteData.Values["action"].ToString().ToUpper();
            string controller = context.RouteData.Values["controller"].ToString().ToUpper();
            if (controller != "HOME" && action != "LOGIN")
            {
                if (string.IsNullOrEmpty(session))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "home", action = "login" }));
                }
            }
        }
        /// <summary>
        /// 页面从定向
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
