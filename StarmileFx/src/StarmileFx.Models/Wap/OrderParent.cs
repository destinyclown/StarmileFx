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
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceID { get; set; }
        /// <summary>
        /// 包裹价格
        /// </summary>
        public float PackPrice { get; set; }
        /// <summary>
        /// 快递价格
        /// </summary>
        public float ExpressPrice { get; set; }
        /// <summary>
        /// 订单状态1、待付款2、待发货3、待收货
        /// </summary>
        public OrderStateEnum OrderState { get; set; }
        /// <summary>
        /// 支付类型1、微信支付2、支付宝支付3、货到付款（暂不支持）
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>
        public string ReceiveName { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string CustomerRemarks { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? DeliveryTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }
    }
}
