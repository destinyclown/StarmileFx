using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// SKU创建
    /// </summary>
    [SugarTable("SKUEstablish")]
    public class SKUEstablish : ModelBase
    {
        /// <summary>
        /// 原始SKU
        /// </summary>
        public int OriginalSKU { get; set; }
    }
}
