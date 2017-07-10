namespace StarmileFx.Models.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SysRolePermissions : ModelBase
    {
        public int Permissions { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
    }
}
