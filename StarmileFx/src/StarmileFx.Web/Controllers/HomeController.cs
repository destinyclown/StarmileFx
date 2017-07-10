using System.Threading.Tasks;
using StarmileFx.Common.Encryption;
using StarmileFx.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Base;
using StarmileFx.Web.Filter;
using static StarmileFx.Web.Filter.Authorization;
using Microsoft.Extensions.Configuration;
using StarmileFx.Models;
using StarmileFx.Common;
using StarmileFx.Server.IServices;
using static StarmileFx.Models.Web.HomeFromModel;

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
        [Authorization(_CallType = CallType.Normal)]
        public IActionResult Index()
        {
            ViewData[SysConst.Token] = HttpContext.Session.GetString(SysConst.Token);
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
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录请求
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="validCode"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginFrom fromData)
        {
            if (string.Compare(fromData.validCode, HttpContext.Session.GetString(SysConst.Captcha), true) != 0)
            {
                result.ReasonDescription = "验证码错误！";
                return Json(result);
            }
            fromData.password = Encryption.toMd5(fromData.password);
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
                HttpContext.Session.SetString(SysConst.Token, responseResult.Token);
            }
            return Json(result);
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult Captcha()
        {
            string code = "";
            System.IO.MemoryStream ms = VierificationCode.Create(out code);
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
            HttpContext.Session.Clear();
            ResponseResult<Result> responseResult = await _BaseServer.Logout(token);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
            else
            {
                result = responseResult.Content;
            }
            return Json(result);
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
