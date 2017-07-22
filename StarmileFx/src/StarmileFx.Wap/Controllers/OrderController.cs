using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
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
        public IActionResult OrderPay()
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
    }
}
