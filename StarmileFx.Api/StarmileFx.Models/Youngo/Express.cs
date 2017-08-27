using SqlSugar;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 快递
    /// </summary>
    [SugarTable("Express")]
    public class Express : ModelBase
    {
        /// <summary>
        /// 快递名称
        /// </summary>
        public string ExpressName { get; set; }
        /// <summary>
        /// 快递标识
        /// </summary>
        public string ExpressCode { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 是否停止使用
        /// </summary>
        public bool IsStop { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
