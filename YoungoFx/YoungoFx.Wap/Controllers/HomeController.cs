using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoungoFx.Web.Server.IService;
using StarmileFx.Wap.Services;
using StarmileFx.Models.Redis;

namespace StarmileFx.Wap.Controllers
{
    public class HomeController : BaseController
    {
        //依赖注入
        private readonly IYoungoServer _YoungoServer;

        public HomeController(IYoungoServer IYoungoServer)
        {
            _YoungoServer = IYoungoServer;
        }
        //OrderService o = new OrderService();
        public async Task<IActionResult> Index()
        {
            CacheProductList ProductList = await _YoungoServer.GetCacheProductList();
            ViewBag.Title = "Youngo商城";
            return View(ProductList);
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
