using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 线上订单详情
    /// </summary>
    [SugarTable("OnLineOrderDetail")]
    public class OnLineOrderDetail : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 商品ID（SKU）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
