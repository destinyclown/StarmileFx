using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Youngo;
using StarmileFx.Wap.Server.IService;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Wap.Controllers
{
    public class CustomerController : Controller
    {
        //依赖注入
        private readonly IYoungoServer _YoungoServer;
        public CustomerController(IYoungoServer YoungoServer)
        {
            _YoungoServer = YoungoServer;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
        public async Task<IActionResult> Adress(int customerId)
        {
            List<DeliveryAddress> list = await _YoungoServer.GetDeliveryAddressList(customerId);
            ViewBag.Title = "收件地址";
            return View(list);
        }
        public IActionResult Message()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
        public IActionResult Service()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
        public IActionResult ServiceList()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
        public IActionResult Suggest()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
        public IActionResult TakeGoods(int? adressId)
        {
            DeliveryAddress adress = new DeliveryAddress();
            if (adressId != null)
            {
                ViewBag.Title = "编辑地址";
            }
            else
            {
                ViewBag.Title = "新增地址";
            }
            return View(adress);
        }
        public IActionResult Vip()
        {
            ViewBag.Title = "用户中心";
            return View();
        }
    }
}
