using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Redis;

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
        bool AddShopCart(int CustomerID, Dictionary<string, int> productList);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        bool AddShopCartProduct(int CustomerID, string ProductID);
        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="shopCart"></param>
        /// <returns></returns>
        bool ModifyShopCart(int CustomerID, Dictionary<string, int> productList);
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        bool DeleteShopCart(int CustomerID);
        
    }
}
