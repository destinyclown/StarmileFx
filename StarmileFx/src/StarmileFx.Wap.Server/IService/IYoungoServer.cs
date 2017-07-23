using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Wap.Server.IService
{
    public interface IYoungoServer
    {
        /// <summary>
        /// 创建订单编号
        /// </summary>
        /// <returns></returns>
        string CreateOrderID();
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        ShopCart GetShopCart(int CustomerID);
        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool CreateShopCart(ShopCart shopCart);
        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool ModifyShopCart(ShopCart shopCart);
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        bool ClearShopCart(int CustomerID);
        /// <summary>
        /// 获取缓存中的商品列表
        /// </summary>
        /// <returns></returns>
        Task<CacheProductList> GetCacheProductList();
        /// <summary>
        /// api获取商品列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseResult<CacheProductList>> GetProductList();
        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<DeliveryAddress> GetDefaultAddress(int CustomerId);
        /// <summary>
        /// 获取默认地址
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<List<DeliveryAddress>> GetDeliveryAddressList(int CustomerId);
        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> OrderPay(string orderId);
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        Task<ResponseResult<bool>> CreateOrderParent(ShopCart shopCart);
    }
}
