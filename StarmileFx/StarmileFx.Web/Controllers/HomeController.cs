using System.Threading.Tasks;
using StarmileFx.Common.Encryption;
using StarmileFx.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Base;
using Microsoft.Extensions.Configuration;
using StarmileFx.Models;
using StarmileFx.Common;
using StarmileFx.Web.Server.IServices;
using static StarmileFx.Models.Web.HomeFromModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace StarmileFx.Web.Controllers.Controllers
{
    public class HomeController : BaseController
    {
        //依赖注入
        private readonly IBaseServer _BaseServer;
        private readonly IConfiguration _Configuration;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="IBaseServer"></param>
        public HomeController(IConfiguration configuration, IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
            _Configuration = configuration;
        }

        #region 首页模块
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        //[Authorize(Policy = "Authorize")]
        public IActionResult Index()
        {
            Token = User.Identities.First(u => u.IsAuthenticated).FindFirst("Token").Value;
            ViewData[SysConst.Token] = Token;
            return View();
        }
        public IActionResult Default()
        {
            return View();
        }
        #endregion

        #region 登录模块
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录请求
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginFrom fromData)
        {
            if (string.Compare(fromData.validCode, HttpContext.Session.GetString(SysConst.Captcha), true) != 0)
            {
                result.ReasonDescription = "验证码错误！";
                return Json(result);
            }
            fromData.password = Encryption.ToMd5(fromData.password);
            fromData.ip = HttpContext.Connection.RemoteIpAddress.ToString();
            ResponseResult<Result> responseResult = await _BaseServer.Login(fromData);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
            else
            {
                result = responseResult.Content;
                var claims = new List<Claim>()
                {
                    new Claim("Token", responseResult.Token),
                    new Claim(ClaimTypes.Name, fromData.loginName)
                };
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, result.ReasonDescription));
                await HttpContext.SignInAsync("TOKEN_COOKIE_NAME", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(6),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
            }
            return Json(result);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Captcha()
        {
            System.IO.MemoryStream ms = VierificationCode.Create(out string code);
            HttpContext.Session.SetString(SysConst.Captcha, code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            Token = User.Identities.First(u => u.IsAuthenticated).FindFirst("Token").Value;
            HttpContext.Session.Clear();
            ResponseResult<Result> responseResult = await _BaseServer.Logout(Token);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
            else
            {
                result = responseResult.Content;
                await HttpContext.SignOutAsync("TOKEN_COOKIE_NAME");
            }
            return Json(result);
        }
        #endregion

        #region 错误页面
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Maintain()
        {
            return View();
        }
        #endregion
    }
}
