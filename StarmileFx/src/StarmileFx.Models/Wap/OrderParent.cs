using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Wap
{
    /// <summary>
    /// 平台订单（临时）
    /// </summary>
    public class OrderParent : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 商品ID（SKU）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 收货地址ID
        /// </summary>
        public int DeliveryAddressID { get; set; }
        /// <summary>
        /// 支付类型1、微信支付2、支付宝支付3、货到付款（暂不支持）
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string CustomerRemarks { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
    }
}
