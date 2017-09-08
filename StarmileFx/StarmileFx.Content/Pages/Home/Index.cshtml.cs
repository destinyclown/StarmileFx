using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace StarmileFx.Content.Pages.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            Toekn = User.Identities.First(u => u.IsAuthenticated).FindFirst("Token").Value;
        }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Toekn { get; set; }
    }
}
