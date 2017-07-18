using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 线上订单
    /// </summary>
    public class OnLineOrderParent : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceID { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchProcessingID { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string DeliveryUser { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 收货地址ID
        /// </summary>
        public int DeliveryAddressID { get; set; }
        /// <summary>
        /// 订单状态1、待付款2、待发货3、待收货
        /// </summary>
        public OrderStateEnum OrderState { get; set; }
        /// <summary>
        /// 支付类型1、微信支付2、支付宝支付3、货到付款（暂不支持）
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }
        /// <summary>
        /// 包裹总量
        /// </summary>
        public float Weight { get; set; }
        /// <summary>
        /// 邮寄方式ID
        /// </summary>
        public int PostID { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 包裹价格
        /// </summary>
        public float PackPrice { get; set; }
        /// <summary>
        /// 快递价格
        /// </summary>
        public float ExpressPrice { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string CustomerRemarks { get; set; }
        /// <summary>
        /// 是否发货
        /// </summary>
        public bool IsDelivery { get; set; }
        /// <summary>
        /// 是否延迟发货
        /// </summary>
        public bool IsDelay { get; set; }
        /// <summary>
        /// 是否偏远地区
        /// </summary>
        public bool IsRemoteArea { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelet { get; set; }
        /// <summary>
        /// 是否易碎
        /// </summary>
        public int IsFragile { get; set; }
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
