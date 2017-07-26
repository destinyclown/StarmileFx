using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using StarmileFx.Wap.Server.IService;
using static StarmileFx.Models.Wap.WapFrom;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Wap.Controllers
{
    public class OrderController : BaseController
    {
        //依赖注入
        private readonly IYoungoServer _YoungoServer;
        public OrderController(IYoungoServer YoungoServer)
        {
            _YoungoServer = YoungoServer;
        }
        // GET: /<controller>/
        public IActionResult Index(string orderId)
        {
            ViewBag.Title = "查看订单";
            return View();
        }

        public IActionResult OrderPay()
        {
            ViewBag.Title = "确认订单";
            return View();
        }

        public IActionResult Evaluate()
        {
            ViewBag.Title = "确认订单";
            return View();
        }
        public IActionResult OrderList()
        {
            ViewBag.Title = "确认订单";
            return View();
        }
        public IActionResult PayError()
        {
            ViewBag.Title = "确认订单";
            return View();
        }
        public IActionResult PaySuccess()
        {
            ViewBag.Title = "确认订单";
            return View();
        }

        public IActionResult ShoppingCart()
        {
            ViewBag.Title = "购物车";
            return View();
        }

        /// <summary>
        /// 提交购物车
        /// </summary>
        /// <param name="fromData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitCart([FromForm]ShoppingCartFrom fromData)
        {
            CacheProductList ProductList = await _YoungoServer.GetCacheProductList();
            DeliveryAddress DeliveryAddress = await _YoungoServer.GetDefaultAddress(fromData.CustomerID);
            ShopCart cart = new ShopCart();
            float TotalPrice = 0;
            string[] isCheck = fromData.CartCheck.Split(',');
            string[] productID = fromData.ProductID.Split(',');
            string[] _number = fromData.Number.Split(',');
            if (DeliveryAddress != null)
            {
                cart.Address = DeliveryAddress;
            }
            cart.CustomerID = fromData.CustomerID;
            cart.OrderState = OrderStateEnum.WaitPayment;
            cart.PaymentType = PaymentTypeEnum.WeChatPayment;
            for (int i = 0; i < isCheck.Count(); i++)
            {
                if (isCheck[i] == "true")
                {
                    Product product = ProductList.ProductList.Where(a => a.ProductID == productID[i]).ToList()[0];
                    int number = int.Parse(_number[i]);
                    ProductList _product = new ProductList();
                    _product.Number = number;
                    _product.Product = product;
                    _product.ProductID = productID[i];
                    _product.TotalPrice = number * product.PurchasePrice;
                    cart.ProductList.Add(_product);
                    TotalPrice += TotalPrice;
                }
            }
            cart.ProductPrice = TotalPrice;
            //运费
            cart.FreightPrice = 0;
            cart.TotalPrice = cart.ProductPrice + cart.FreightPrice;
            return View("Index", cart);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrder(ShopCart shopCart)
        {
            ViewBag.Title = "微信支付";
            if (shopCart == null)
            {
                result.ReasonDescription = "购物车为空！";
                return Json(result);
            }
            shopCart.OrderId = _YoungoServer.CreateOrderID();
            ResponseResult<bool> responseResult = await _YoungoServer.OrderCreate(shopCart);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return View(result);
            }
            else
            {
                result.IsSuccessful = responseResult.Content;
            }
            return View(result);
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OrderPay(string orderId)
        {
            ResponseResult<bool> responseResult = await _YoungoServer.OrderPay(orderId);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
            else
            {
                result.IsSuccessful = responseResult.Content;
            }
            return Json(result);
        }
    }
}
