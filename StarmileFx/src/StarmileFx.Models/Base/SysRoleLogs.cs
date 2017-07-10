namespace StarmileFx.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SysRoleLogs : ModelBase
    {
        public int RoleID { get; set; }
        public string LoginIP { get; set; }

    }
}
