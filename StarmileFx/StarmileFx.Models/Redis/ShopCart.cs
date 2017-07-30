using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;
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
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public DeliveryAddress Address { get; set; }
        /// <summary>
        /// 订单状态1、待付款2、待发货3、待收货
        /// </summary>
        public OrderStateEnum OrderState { get; set; }
        /// <summary>
        /// 支付类型1、微信支付2、支付宝支付3、货到付款（暂不支持）
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string CustomerRemarks { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<ProductList> ProductList { get; set; }
        /// <summary>
        /// 商品总价格
        /// </summary>
        public float ProductPrice { get; set; }
        /// <summary>
        /// 购物车总价格
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public float FreightPrice { get; set; }
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
