using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 浏览历史
    /// </summary>
    [SugarTable("ViewHistory")]
    public class ViewHistory : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? CustomerID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductID { get; set; }
    }
}
