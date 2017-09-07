﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace StarmileFx.Content.Pages
{
    public class ContainerModel : PageModel
    {
        public void OnGet()
        {
            UserName = User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Name).Value;
        }

        public string UserName { get; set; }
    }
}
