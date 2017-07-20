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
            ViewBag.Title = "Youngo商城";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            ViewBag.Title = "购物车";
            return View();
        }

        public IActionResult Category()
        {
            ViewBag.Title = "分类列表";
            return View();
        }

        public IActionResult Search()
        {
            ViewBag.Title = "搜索页";
            return View();
        }

        public IActionResult UserCenter()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
    }
}
