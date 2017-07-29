using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarmileFx.Common;
using StarmileFx.Common.Encryption;
using StarmileFx.Models;
using StarmileFx.Models.Enum;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using StarmileFx.Wap.Server.IService;
using StarmileFx.Wap.Server.IServices;

namespace StarmileFx.Wap.Server.Service
{
    public class YoungoManager : IYoungoServer
    {
        private readonly IRedisServer _IRedisServer;
        private string Api_Host;
        HttpHelper httpHelper = new HttpHelper();
        public YoungoManager(IRedisServer IRedisServer)
        {
            _IRedisServer = IRedisServer;
            _IRedisServer.conn = "127.0.0.1";
            Api_Host = "http://localhost:8001/";//测试使用
        }
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <returns></returns>
        public string CreateOrderID()
        {
            Random ran = new Random();
            int RandKey = ran.Next(0, 999999);
            string OrderID = "Y0000" + DateTime.Now.ToString("yyMMdd") + RandKey.ToString("000000");
            return OrderID;
        }

        #region 购物车ShopCart
        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public bool CreateShopCart(ShopCart shopCart)
        {
            string key = "ShopCart_" + shopCart.CustomerID.ToString();
            if (!_IRedisServer.KeyExists(key))
            {
                return _IRedisServer.SetStringKey(key, shopCart);
            }
            else
            {
                throw new Exception("用户购物车已存在！");
            }
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ShopCart GetShopCart(int CustomerID)
        {
            string key = "ShopCart_" + CustomerID.ToString();
            ShopCart shopCart = new ShopCart();
            if (_IRedisServer.KeyExists(key))
            {
                shopCart = _IRedisServer.GetStringKey<ShopCart>(key);
            }
            else
            {
                return null;
            }
            return shopCart;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public bool ClearShopCart(int CustomerID)
        {
            string key = "ShopCart_" + CustomerID.ToString();
            if (_IRedisServer.KeyDelete(key))
            {
                return true;
            }
            else
            {
                throw new Exception("删除购物车失败！");
            }
        }

        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public bool ModifyShopCart(ShopCart shopCart)
        {
            string key = "ShopCart_" + shopCart.CustomerID.ToString();
            if (_IRedisServer.KeyDelete(key))
            {
                return _IRedisServer.SetStringKey(key, shopCart);
            }
            else
            {
                throw new Exception("删除购物车失败！");
            }
        }

        /// <summary>
        /// 创建购物车（临时10分钟）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public bool CreateTemporaryShopCart(ShopCart shopCart)
        {
            string key = "TemporaryCart_" + shopCart.CustomerID.ToString();
            TimeSpan time = DateTime.Now.AddMinutes(10) - DateTime.Now;
            if (!_IRedisServer.KeyExists(key))
            {
                return _IRedisServer.SetStringKey(key, shopCart, time);
            }
            else
            {
                if (_IRedisServer.KeyDelete(key))
                {
                    return _IRedisServer.SetStringKey(key, shopCart, time);
                }
                else
                {
                    throw new Exception("删除临时购物车失败！");
                }
            }
        }

        /// <summary>
        /// 获取购物车（临时10分钟）
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ShopCart GetTemporaryShopCart(int CustomerID)
        {
            string key = "TemporaryCart_" + CustomerID.ToString();
            ShopCart shopCart = new ShopCart();
            if (_IRedisServer.KeyExists(key))
            {
                shopCart = _IRedisServer.GetStringKey<ShopCart>(key);
            }
            else
            {
                return null;
            }
            return shopCart;
        }
        #endregion 购物车ShopCart

        #region 网站商品缓存CacheProductList
        /// <summary>
        /// 获取缓存中的商品列表
        /// </summary>
        /// <returns></returns>
        public Task<CacheProductList> GetCacheProductList()
        {
            return Task.Run(() =>
            {
                return GetCacheProductListAsync();
            });
        }

        /// <summary>
        /// 获取缓存中的商品列表（异步）
        /// </summary>
        /// <returns></returns>
        public async Task<CacheProductList> GetCacheProductListAsync()
        {
            string key = "CacheProduct";
            if (_IRedisServer.KeyExists(key))
            {
                return _IRedisServer.GetStringKey<CacheProductList>(key);
            }
            var result = await GetProductList();
            TimeSpan time = DateTime.Now.AddMinutes(15) - DateTime.Now;
            if (result != null && result.IsSuccess)
            {
                if(_IRedisServer.SetStringKey(key, result.Content, time))
                {
                    return result.Content;
                }
                else
                {
                    throw new Exception("添加redis缓存失败！");
                }
            }
            else
            {
                throw new Exception(result.ErrorMsg);
            }
        }

        /// <summary>
        /// api获取商品列表
        /// </summary>
        /// <returns></returns>
        public Task<ResponseResult<CacheProductList>> GetProductList()
        {
            return Task.Run(() =>
            {
                return GetProductListAsync();
            });
        }

        /// <summary>
        /// api获取商品列表（异步）
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<CacheProductList>> GetProductListAsync()
        {
            string Action = "Youngo";
            string Function = "/GetCacheProductList";
            string Parameters = "";
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<CacheProductList>>(result);
            });
        }
        #endregion 网站商品缓存CacheProductList

        #region 用户中心
        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public Task<DeliveryAddress> GetDefaultAddress(int CustomerId)
        {
            return Task.Run(() =>
            {
                return GetDefaultAddressAsync(CustomerId);
            });
        }

        /// <summary>
        /// 获取用户默认地址（异步）
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<DeliveryAddress> GetDefaultAddressAsync(int CustomerId)
        {
            string Action = "Youngo";
            string Function = "/GetDefaultAddress";
            string Parameters = string.Format("CustomerId={0}", CustomerId);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<DeliveryAddress>(result);
            });
        }

        /// <summary>
        /// 获取用户地址列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public Task<ResponseResult<List<DeliveryAddress>>> GetDeliveryAddressList(int CustomerId)
        {
            return Task.Run(() =>
            {
                return GetDeliveryAddressListAsync(CustomerId);
            });
        }

        /// <summary>
        /// 获取用户地址列表（异步）
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<DeliveryAddress>>> GetDeliveryAddressListAsync(int CustomerId)
        {
            string Action = "Youngo";
            string Function = "/GetDeliveryAddressList";
            string Parameters = string.Format("CustomerId={0}", CustomerId);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<DeliveryAddress>>>(result);
            });
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public Task<ResponseResult<List<Information>>> GetMessageList(int CustomerId, int PageSize, int PageIndex)
        {
            return Task.Run(() =>
            {
                return GetMessageListAsync(CustomerId, PageSize, PageIndex);
            });
        }

        /// <summary>
        /// 获取消息列表（异步）
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<Information>>> GetMessageListAsync(int CustomerId, int PageSize, int PageIndex)
        {
            string Action = "Youngo";
            string Function = "/GetMessageList";
            string Parameters = string.Format("CustomerId={0}&PageSize={1}&PageIndex={2}", CustomerId, PageSize, PageIndex);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<Information>>>(result);
            });
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public Task<ResponseResult<Customer>> GetCustomer(string WeCharKey)
        {
            return Task.Run(() =>
            {
                return GetCustomerAsync(WeCharKey);
            });
        }

        /// <summary>
        /// 获取用户信息（异步）
        /// </summary>
        /// <param name="WeCharKey"></param>
        /// <returns></returns>
        public async Task<ResponseResult<Customer>> GetCustomerAsync(string WeCharKey)
        {
            string Action = "Youngo";
            string Function = "/GetCustomer";
            string Parameters = string.Format("WeCharKey={0}", WeCharKey);
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.GET, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject< ResponseResult<Customer>>(result);
            });
        }
        #endregion 用户中心

        #region 订单
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public Task<ResponseResult<bool>> OrderCreate(ShopCart shopCart)
        {
            return Task.Run(() =>
            {
                return OrderCreateAsync(shopCart);
            });
        }

        /// <summary>
        /// 创建订单（异步）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> OrderCreateAsync(ShopCart shopCart)
        {
            string Action = "Youngo";
            string Function = "/OrderCreate";
            string Parameters = string.Empty;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select, shopCart);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public Task<ResponseResult<bool>> OrderPay(string orderId, string TransactionId)
        {
            return Task.Run(() =>
            {
                return OrderPayAsync(orderId, TransactionId);
            });
        }

        /// <summary>
        /// 确认订单（异步）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> OrderPayAsync(string orderId, string TransactionId)
        {
            string Action = "Youngo";
            string Function = "/OrderPay";
            string Parameters = string.Format("orderId={0}&TransactionId={1}", orderId, TransactionId); ;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public Task<ResponseResult<bool>> OrderCancel(string orderId)
        {
            return Task.Run(() =>
            {
                return OrderCancelAsync(orderId);
            });
        }

        /// <summary>
        /// 取消订单（异步）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> OrderCancelAsync(string orderId)
        {
            string Action = "Youngo";
            string Function = "/OrderCancel";
            string Parameters = string.Format("orderId={0}", orderId); ;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public Task<ResponseResult<bool>> OrderDelete(string orderId)
        {
            return Task.Run(() =>
            {
                return OrderDeleteAsync(orderId);
            });
        }

        /// <summary>
        /// 删除订单（异步）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> OrderDeleteAsync(string orderId)
        {
            string Action = "Youngo";
            string Function = "/OrderCancel";
            string Parameters = string.Format("orderId={0}&IsDelete=true", orderId); ;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        /// <summary>
        /// 完成订单（确认收货）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public Task<ResponseResult<bool>> OrderComplete(string orderId)
        {
            return Task.Run(() =>
            {
                return OrderCompleteAsync(orderId);
            });
        }

        /// <summary>
        /// 完成订单（异步）
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        public async Task<ResponseResult<bool>> OrderCompleteAsync(string orderId)
        {
            string Action = "Youngo";
            string Function = "/OrderComplete";
            string Parameters = string.Format("orderId={0}", orderId); ;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<bool>>(result);
            });
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public Task<ResponseResult<List<OrderParent>>> GetOrderParentcsList(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex)
        {
            return Task.Run(() =>
            {
                return GetOrderParentcsListAsync(OrderState, CustomerId, PageSize, PageIndex);
            });
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="OrderState"></param>
        /// <param name="CustomerId"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public async Task<ResponseResult<List<OrderParent>>> GetOrderParentcsListAsync(OrderStateEnum OrderState, int CustomerId, int PageSize, int PageIndex)
        {
            string Action = "Youngo";
            string Function = "/GetOrderParentcsList";
            string Parameters = string.Format("OrderState={0}&CustomerId={1}&PageSize={2}&PageIndex={3}", OrderState, CustomerId, PageSize, PageIndex); ;
            string result = await httpHelper.QueryData(Api_Host + Action + Function
                , Parameters, HttpHelper.MethodType.POST, HttpHelper.SelectType.Select);
            return await Task.Run(() =>
            {
                return JsonConvert.DeserializeObject<ResponseResult<List<OrderParent>>>(result);
            });
        }
        #endregion 订单
    }
}
