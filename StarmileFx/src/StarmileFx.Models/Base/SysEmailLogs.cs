namespace StarmileFx.Models.Base
{
    using System.ComponentModel.DataAnnotations;


    public partial class SysEmailLogs : ModelBase
    {
        public string Email { get; set; }
        public string EmailLog { get; set; }
    }
}
