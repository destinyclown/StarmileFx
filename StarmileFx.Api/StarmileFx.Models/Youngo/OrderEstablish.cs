using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 订单创建
    /// </summary>
    [SugarTable("CustomerComment")]
    public class OrderEstablish : ModelBase
    {
        /// <summary>
        /// 原始订单编号
        /// </summary>
        public int OriginalOrderID { get; set; }
    }
}
