namespace StarmileFx.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public partial class PageData
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 是否升序
        /// </summary>
        public bool IsAsc { get; set; }
    }
}
