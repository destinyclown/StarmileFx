using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum CustomerTypeEnum : int
    {
        Ordinary = 1,
        Platinum = 2,
        Gold = 3
    }

    /// <summary>
    /// 订单状态类型
    /// </summary>
    public enum OrderStateEnum : int
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 待付款
        /// </summary>
        WaitPayment = 1,
        /// <summary>
        /// 待发货
        /// </summary>
        WaitShipment = 2,
        /// <summary>
        /// 待收货
        /// </summary>
        WaitDelivery = 3,
        /// <summary>
        /// 已完成
        /// </summary>
        Completed = 4,
        /// <summary>
        /// 取消订单
        /// </summary>
        Canceled = 5,
        /// <summary>
        /// 申请退款
        /// </summary>
        ApplyRefund = 6,
        /// <summary>
        /// 申请退货
        /// </summary>
        ApplyReturns = 7,
        /// <summary>
        /// 申请换货
        /// </summary>
        ApplyExchange = 8,
        /// <summary>
        /// 退款完成
        /// </summary>
        Refunded = 9,
        /// <summary>
        /// 退货完成
        /// </summary>
        Returned = 10,
        /// <summary>
        /// 换货完成
        /// </summary>
        Exchanged = 11
    }

    /// <summary>
    /// 支付类型
    /// </summary>
    public enum PaymentTypeEnum : int
    {
        /// <summary>
        /// 现金支付
        /// </summary>
        CashPayment = 0,
        /// <summary>
        /// 微信支付
        /// </summary>
        WeChatPayment = 1,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        AlipayPayment = 2,
        /// <summary>
        /// 货到付款（暂不支付）
        /// </summary>
        DeliveryPayment = 3
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public enum OrderTypeEnum : int
    {

    }

    /// <summary>
    /// 积分类型
    /// </summary>
    public enum SignEnum : int
    {
        /// <summary>
        /// 会员签到10点积分
        /// </summary>
        会员签到10点积分 = 10,
        /// <summary>
        /// 购买商品20点积分
        /// </summary>
        购买商品20点积分 = 20,
        /// <summary>
        /// 添加评论5点积分
        /// </summary>
        添加评论5点积分 = 5,
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourcesEnum : int
    {
        /// <summary>
        /// 商品类型
        /// </summary>
        Product = 1,
        /// <summary>
        /// 商品评论
        /// </summary>
        Comment = 1,
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageTypeEnum : int
    {
        /// <summary>
        /// 订单确认通知
        /// </summary>
        Confirm = 1,
        /// <summary>
        /// 订单取消通知
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// 订单发货通知
        /// </summary>
        Shipment = 3,
        /// <summary>
        /// 订单申请退款通知
        /// </summary>
        ApplyRefund = 4,
        /// <summary>
        /// 订单申请退货通知
        /// </summary>
        ApplyReturns = 5,
        /// <summary>
        /// 订单申请换货通知
        /// </summary>
        ApplyExchange = 6,
        /// <summary>
        /// 订单退款完成通知
        /// </summary>
        Refunded = 7,
        /// <summary>
        /// 订单退货完成通知
        /// </summary>
        Returned = 8,
        /// <summary>
        /// 订单换货完成通知
        /// </summary>
        Exchanged = 9
    }
}
