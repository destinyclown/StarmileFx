using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
using StarmileFx.Models.Base;
using StarmileFx.Models.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Content.Controllers
{
    [Route("Account")]
    public class AccountController : BaseController
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
            //if (string.Compare(fromData.validCode, HttpContext.Session.GetString(SysConst.Captcha), true) != 0)
            //{
            //    result.ReasonDescription = "验证码错误！";
            //    return Json(result);
            //}
            fromData.Password = Encryption.ToMd5(fromData.Password);
            fromData.Ip = HttpContext.Connection.RemoteIpAddress.ToString();
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
                    new Claim(fromData.Email, responseResult.Token),
                    new Claim(ClaimTypes.Name, fromData.Email)
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
        public async Task<IActionResult> Logout(string email)
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst(email).Value;
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

        #region 菜单操作
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("GetMenuJson")]
        public async Task<string> GetMenuJson(string email)
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst(email).Value;
            ResponseResult<List<WebMenus>> responseResult = await _BaseServer.GetMenuJson(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = responseResult.Content,
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseJsonp(funcAction);
        }

        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("GetCollectionList")]
        public async Task<string> GetCollectionList(string email)
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst(email).Value;
            ResponseResult<List<SysCollection>> responseResult = await _BaseServer.GetCollectionList(Token);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    Content = new
                    {
                        UserId = email,
                        MenuList = responseResult.Content
                    },
                    IsSuccess = true
                };
                return responseModel;
            };
            return ActionResponseJsonp(funcAction);
        }

        /// <summary>
        /// 提交收藏
        /// </summary>
        /// <param name="email"></param>
        /// <param name="MenuKey"></param>
        /// <param name="MenuUrl"></param>
        /// <param name="MenuName"></param>
        /// <param name="MenuContent"></param>
        /// <returns></returns>
        [Route("ConfirmCollection")]
        public async Task<string> ConfirmCollection(string email, int MenuKey, string MenuUrl, string MenuName,string MenuContent)
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst(email).Value;
            WebCollection collection = new WebCollection
            {
                Token = Token,
                MenuContent = MenuContent,
                MenuUrl = MenuUrl,
                MenuKey = MenuKey,
                MenuName = MenuName
            };
            ResponseResult<Result> responseResult = await _BaseServer.ConfirmCollection(collection);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    IsSuccess = responseResult.Content.IsSuccess
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="email"></param>
        /// <param name="MenuKey"></param>
        /// <param name="MenuName"></param>
        /// <returns></returns>
        [Route("CancelCollection")]
        public async Task<string> CancelCollection(string email, int MenuKey, string MenuName)
        {
            string Token = User.Identities.First(u => u.IsAuthenticated).FindFirst(email).Value;
            WebCollection collection = new WebCollection
            {
                Token = Token,
                MenuKey = MenuKey,
                MenuName = MenuName
            };
            ResponseResult<Result> responseResult = await _BaseServer.CancelCollection(collection);
            Func<ResponseResult> funcAction = () =>
            {
                var responseModel = new ResponseResult
                {
                    IsSuccess = responseResult.Content.IsSuccess
                };
                return responseModel;
            };
            return ActionResponseGetString(funcAction);
        }
        #endregion
    }
}
