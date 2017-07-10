namespace StarmileFx.Models.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public partial class SysRoles : ModelBase 
    {
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public int Permissions { get; set; }
        public string Url { get; set; }
    }
}
