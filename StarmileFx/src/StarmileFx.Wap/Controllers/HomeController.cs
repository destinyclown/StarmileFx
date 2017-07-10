using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Wap.Services;

namespace StarmileFx.Wap.Controllers
{
    public class HomeController : Controller
    {
        OrderService o = new OrderService();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
