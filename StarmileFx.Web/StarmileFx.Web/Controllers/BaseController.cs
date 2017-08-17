﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StarmileFx.Web.Controllers.Controllers;
using StarmileFx.Models;
using Microsoft.AspNetCore.Authorization;

namespace StarmileFx.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 返回结果对象
        /// </summary>
        public Result result = new Result();

        /// <summary>
        /// Token令牌
        /// </summary>
        public string Token { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            result = new Result();
            
            base.OnActionExecuting(context);
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
