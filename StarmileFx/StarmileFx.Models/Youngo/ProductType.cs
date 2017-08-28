using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarTable("ProductType")]
    public class ProductType : ModelBase
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
