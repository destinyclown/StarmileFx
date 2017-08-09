using StarmileFx.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using StarmileFx.Web.Controllers.Controllers;
using StarmileFx.Models;
using System.Security.Claims;

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
            
            base.OnActionExecuting(context);
            var authentication = HttpContext.Authentication.GetAuthenticateInfoAsync("Cookie").Result.Principal;
            if (authentication == null || string.IsNullOrEmpty(authentication.FindFirst(ClaimTypes.Name).Value))
            {
                string action = context.RouteData.Values["action"].ToString().ToLower();
                string controller = context.RouteData.Values["controller"].ToString().ToLower();
                if (controller != "home" || (action != "login" && action != "captcha"))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "home", action = "login" }));
                }
            }
            //string session = "";
            //if (context.HttpContext.Session != null)
            //{
            //    session = context.HttpContext.Session.GetString(SysConst.Token);
            //}

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
