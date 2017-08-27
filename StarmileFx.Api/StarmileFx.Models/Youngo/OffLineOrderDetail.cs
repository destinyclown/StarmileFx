using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 线下订单详情
    /// </summary>
    [SugarTable("OffLineOrderDetail")]
    public class OffLineOrderDetail : ModelBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 商品ID(SKU)
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
