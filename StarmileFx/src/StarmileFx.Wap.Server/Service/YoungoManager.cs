using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarmileFx.Common;
using StarmileFx.Common.Encryption;
using StarmileFx.Models;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
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
            Api_Host = "http://localhost:8001/";//测试使用
        }
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <returns></returns>
        public string CreateOrderID()
        {
            Random ran = new Random();
            int RandKey = ran.Next(0, 9999);
            string OrderID = "A" + Encryption.StrInCoded(DateTime.Now.ToString("yyyyMMddHHmmssffff")) + RandKey.ToString("0000");
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
        /// 获取购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ShopCart GetShopCart(int CustomerID)
        {
            string key = "ShopCart_" + CustomerID.ToString();
            ShopCart shopCart = _IRedisServer.GetStringKey<ShopCart>(key);
            return shopCart;
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
            TimeSpan time = DateTime.Now - DateTime.Now.AddMinutes(15);
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
    }
}
