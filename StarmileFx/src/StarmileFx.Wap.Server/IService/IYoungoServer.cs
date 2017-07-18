using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;

namespace StarmileFx.Wap.Server.IService
{
    public interface IYoungoServer
    {
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
    }
}
