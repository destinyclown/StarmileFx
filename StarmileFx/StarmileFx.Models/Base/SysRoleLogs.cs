using SqlSugar;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysRoleLogs")]
    public class SysRoleLogs : ModelBase
    {
        public int RoleID { get; set; }
        public string LoginIP { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

    }
}
