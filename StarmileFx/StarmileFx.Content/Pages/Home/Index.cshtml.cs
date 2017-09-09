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
            UserEmail = User.Identity.Name;
        }

        /// <summary>
        /// 用户Email
        /// </summary>
        public string UserEmail { get; set; }
    }
}
