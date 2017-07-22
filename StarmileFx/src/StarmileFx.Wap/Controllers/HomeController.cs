using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Wap.Services;

namespace StarmileFx.Wap.Controllers
{
    public class HomeController : BaseController
    {
        OrderService o = new OrderService();
        public IActionResult Index()
        {
            ViewBag.Title = "Youngo商城";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }        

        public IActionResult Search()
        {
            ViewBag.Title = "搜索页";
            return View();
        }
    }
}
