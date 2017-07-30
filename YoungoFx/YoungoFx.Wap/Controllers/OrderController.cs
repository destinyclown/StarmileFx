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
using YoungoFx.Web.Server.IService;
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

        public IActionResult Evaluate(string orderId)
        {
            ViewBag.Title = "评价订单";
            return View();
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public async Task<IActionResult> OrderList(OrderStateEnum OrderState, int CustomerId, int PageSize = 20, int PageIndex = 1)
        {
            ViewBag.Title = "订单列表";
            ViewBag.CustomerId = CustomerId;
            ResponseResult<List<OrderParent>> responseResult = await _YoungoServer.GetOrderParentcsList(OrderState, CustomerId, PageSize, PageIndex);
            List<OrderParent> list = new List<OrderParent>();
            if (responseResult.IsSuccess)
            {
                list = responseResult.Content;
            }
            return View(list);
        }

        public IActionResult PayError(string orderId)
        {
            ViewBag.Title = "支付失败";
            return View();
        }
        public IActionResult PaySuccess(string orderId)
        {
            ViewBag.Title = "支付成功";
            return View();
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddProductCart(int customerId, string productId)
        {
            CacheProductList list = await _YoungoServer.GetCacheProductList();
            if (_YoungoServer.IsExistenceCart(customerId))
            {
                ShopCart cart = _YoungoServer.GetShopCart(customerId);
                if (cart.ProductList.All(a => a.ProductID == productId))
                {
                    cart.ProductList.Find(a => a.ProductID == productId).Number++;
                }
                else
                {
                    
                }
            }
            return View(result);
        }

        /// <summary>
        /// 购物车
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IActionResult ShoppingCart(int? customerId)
        {
            ViewBag.Title = "购物车";
            ShopCart cart = new ShopCart();
            int CustomerId = 0;
            if (customerId != null)
            {
                CustomerId = int.Parse(customerId.ToString());
                ViewBag.CustomerId = CustomerId;
                cart = _YoungoServer.GetShopCart(CustomerId);
            }
            else
            {
                return View(null);
            }
            return View(cart);
        }

        public IActionResult OrderConfirm()
        {
            ViewBag.Title = "确认订单";
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
            DeliveryAddress DeliveryAddress = new DeliveryAddress();
            ResponseResult<DeliveryAddress> responseResult = await _YoungoServer.GetDefaultAddress(fromData.CustomerID);
            if (responseResult.IsSuccess)
            {
                DeliveryAddress = responseResult.Content;
            }
            ShopCart cart = new ShopCart();
            float TotalPrice = 0;
            string[] isCheck = fromData.CartCheck.Split(',');
            string[] productID = fromData.ProductID.Split(',');
            string[] _number = fromData.Number.Split(',');
            if (DeliveryAddress != null && DeliveryAddress != new DeliveryAddress())
            {
                cart.Address = DeliveryAddress;
            }
            cart.OrderId = _YoungoServer.CreateOrderID();
            cart.CustomerID = fromData.CustomerID;
            cart.OrderState = OrderStateEnum.WaitPayment;
            cart.PaymentType = PaymentTypeEnum.WeChatPayment;
            for (int i = 0; i < isCheck.Count(); i++)
            {
                if (isCheck[i] == "true")
                {
                    ProductModel mode = ProductList.ProductList.Where(a => a.ProductID == productID[i]).ToList()[0];
                    int number = int.Parse(_number[i]);
                    ProductList _product = new ProductList();
                    _product.Number = number;
                    _product.Product = new Product();// product;
                    _product.ProductID = productID[i];
                    _product.TotalPrice = number * 0;//product.PurchasePrice;
                    cart.ProductList.Add(_product);
                    TotalPrice += TotalPrice;
                }
            }
            cart.ProductPrice = TotalPrice;
            //运费
            cart.FreightPrice = 0;
            cart.TotalPrice = cart.ProductPrice + cart.FreightPrice;
            _YoungoServer.CreateTemporaryShopCart(cart);
            return View("OrderConfirm", cart);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrder(int customerId)
        {
            ViewBag.Title = "微信支付";
            ViewBag.CustomerId = customerId;
            ShopCart _ShopCart = _YoungoServer.GetTemporaryShopCart(customerId);
            if (_ShopCart == null)
            {
                result.ReasonDescription = "购物车为空！";
                return Json(result);
            }
            ResponseResult<bool> responseResult = await _YoungoServer.OrderCreate(_ShopCart);
            if (!responseResult.IsSuccess)
            {
                result.ReasonDescription = responseResult.ErrorMsg;
                return View(result);
            }
            else
            {
                ViewBag.Cart = _ShopCart;
                result.IsSuccessful = responseResult.Content;
            }
            return View(result);
        }

        /// <summary>
        /// 确认订单                                                                      
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OrderPay(string orderId, string TransactionId)
        {
            ResponseResult<bool> responseResult = await _YoungoServer.OrderPay(orderId, TransactionId);
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
