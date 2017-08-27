using SqlSugar;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysMenus")]
    public partial class SysRolePermissions : ModelBase
    {
        public int Permissions { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
