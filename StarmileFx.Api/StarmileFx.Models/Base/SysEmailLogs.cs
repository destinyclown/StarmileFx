namespace StarmileFx.Models.Base
{
    using SqlSugar;

    [SugarTable("SysMenus")]
    public partial class SysEmailLogs : ModelBase
    {
        public string Email { get; set; }
        public string EmailLog { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
