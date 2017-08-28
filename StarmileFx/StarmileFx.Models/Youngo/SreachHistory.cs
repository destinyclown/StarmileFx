using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 商品
    /// </summary>
    [SugarTable("SreachHistory")]
    public class SreachHistory : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? CustomerID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
