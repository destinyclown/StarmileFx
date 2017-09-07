using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static StarmileFx.Models.Web.HomeFromModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StarmileFx.Web.Server.IServices;
using StarmileFx.Models;
using StarmileFx.Common.Encryption;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StarmileFx.Common;
using StarmileFx.Common.Enum;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Content.Controllers
{
    [Authorize]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IBaseServer _BaseServer;
        private readonly IConfiguration _Configuration;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="IBaseServer"></param>
        public AccountController(IConfiguration configuration, IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
            _Configuration = configuration;
        }
        /// <summary>
        /// 返回结果对象
        /// </summary>
        public Result result = new Result();

        #region 登录模块
        /// <summary>
        /// 登录请求
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("Login")]
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
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(6),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
            }
            string UserName = User.Claims.FirstOrDefault().Value;
            return Json(result);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Captcha")]
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
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst("Token").Value;
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
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return Json(result);
        }
        #endregion
    }
}
