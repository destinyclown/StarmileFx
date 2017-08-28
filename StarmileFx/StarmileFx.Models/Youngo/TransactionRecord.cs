using StarmileFx.Models.Enum;
using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 交易记录
    /// </summary>
    [SugarTable("TransactionRecord")]
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
