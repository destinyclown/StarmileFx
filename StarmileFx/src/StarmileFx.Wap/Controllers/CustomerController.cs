using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models;
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

        /// <summary>
        /// 会员中心
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string WeCharKey)
        {
            ViewBag.Title = "用户中心";
            Customer model = new Customer();
            if (WeCharKey != null)
            {

                ResponseResult<Customer> responseResult = await _YoungoServer.GetCustomer(WeCharKey);
                if (responseResult.IsSuccess)
                {
                    model = responseResult.Content;
                }
                else
                {
                    return View(null);
                }
            }
            else
            {
                return View(null);
            }
            return View(model);
        }
        public async Task<IActionResult> AdressList(int customerId)
        {
            ViewBag.Title = "收件地址";
            List<DeliveryAddress> list = new List<DeliveryAddress>();
            ResponseResult<List<DeliveryAddress>> responseResult = await _YoungoServer.GetDeliveryAddressList(customerId);
            if (responseResult.IsSuccess)
            {
                list = responseResult.Content;
            }
            else
            {
                return View(null);
            }
            return View(list);
        }
        public async Task<IActionResult> Message(int customerId, int PageSize, int PageIndex)
        {
            ViewBag.Title = "系统消息";
            List<Information> list = new List<Information>();
            ResponseResult<List<Information>> responseResult = await _YoungoServer.GetMessageList(customerId, PageSize, PageIndex);
            if (responseResult.IsSuccess)
            {
                list = responseResult.Content;
            }
            else
            {
                return View(null);
            }
            return View(list);
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
