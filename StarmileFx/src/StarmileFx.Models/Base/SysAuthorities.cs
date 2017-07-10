namespace StarmileFx.Models.Base
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SysAuthorities : ModelBase
    {
        public int PermissionsID { get; set; }
        public string Code { get; set; }
    }
}
