using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Models.Redis
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShopCart
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<ProductList> ProductList { get; set; }
        /// <summary>
        /// 购物车总价格
        /// </summary>
        public float TotalPrice { get; set; }
    }

    /// <summary>
    /// 商品列表
    /// </summary>
    public class ProductList
    {
        /// <summary>
        /// 商品实体类
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 商品ID（SKU）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public float TotalPrice { get; set; }
    }
}
