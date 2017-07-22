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
    /// 订单状态
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
        WaitDelivery = 3
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

    public enum OrderTypeEnum : int
    {

    }

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
}
