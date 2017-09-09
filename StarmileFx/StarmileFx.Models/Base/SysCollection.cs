namespace StarmileFx.Models.Base
{
    using SqlSugar;

    [SugarTable("SysCollection")]
    public class SysCollection : ModelBase
    {
        public int UserId { get; set; }
        public int MenuKey { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuContent { get; set; }
    }
}
