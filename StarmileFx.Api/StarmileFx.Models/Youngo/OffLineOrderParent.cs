using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 线下订单
    /// </summary>
    public class OffLineOrderParent : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public PaymentTypeEnum PaymentType { get; set; }
        /// <summary>
        /// 总价格
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 优惠价
        /// </summary>
        public float DiscountPrice { get; set; }
        /// <summary>
        /// 收款价
        /// </summary>
        public float CollectPrice { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }
    }
}
