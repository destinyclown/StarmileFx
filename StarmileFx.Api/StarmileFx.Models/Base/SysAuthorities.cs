using SqlSugar;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysAuthorities")]
    public partial class SysAuthorities : ModelBase
    {
        public int PermissionsID { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
    }
}
