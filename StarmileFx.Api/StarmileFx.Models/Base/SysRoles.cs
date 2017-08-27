using SqlSugar;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysMenus")]
    public partial class SysRoles : ModelBase 
    {
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public int Permissions { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
