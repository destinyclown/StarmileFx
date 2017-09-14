using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarmileFx.Models.Web;
using System.Threading.Tasks;
using StarmileFx.Common.Encryption;
using StarmileFx.Web.Server.IServices;
using StarmileFx.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System;

namespace StarmileFx.Content.Pages.Home
{
    public class LoginModel : PageModel
    {
        private readonly IBaseServer _BaseServer;
        private readonly IConfiguration _Configuration;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="IBaseServer"></param>
        public LoginModel(IConfiguration configuration, IBaseServer IBaseServer)
        {
            _BaseServer = IBaseServer;
            _Configuration = configuration;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public LoginFrom FromData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                FromData.Password = Encryption.ToMd5(FromData.Password);
                FromData.Ip = HttpContext.Connection.RemoteIpAddress.ToString();
                ResponseResult responseResult = await _BaseServer.Login(FromData);
                if (!responseResult.IsSuccess && responseResult.Error != null)
                {
                    ErrorMessage = responseResult.Error.Message;
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                    return Page();
                }
                else
                {
                    var claims = new List<Claim>()
                {
                    new Claim(FromData.Email, responseResult.Data.ToString()),
                    new Claim(ClaimTypes.Name, FromData.Email)
                };
                    var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, ""));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = FromData.RememberMe ? DateTime.UtcNow.AddHours(3) : DateTime.UtcNow.AddDays(1),
                            IsPersistent = false,
                            AllowRefresh = false
                        });
                }
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}