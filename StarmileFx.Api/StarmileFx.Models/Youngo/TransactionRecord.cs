using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 交易记录
    /// </summary>
    public class TransactionRecord : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 售后类型
        /// </summary>
        public PaymentTypeEnum Type { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public float TotalPrice { get; set; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string TransactionID { get; set; }
    }
}
