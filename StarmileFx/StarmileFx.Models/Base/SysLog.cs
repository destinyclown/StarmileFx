using SqlSugar;

namespace StarmileFx.Models.Base
{
    [SugarTable("SysLog")]
    public class SysLog : ModelBase
    {
        public string Code { get; set; }

        public string Herf { get; set; }

        public string ErrorMessage { get; set; }

        public string Ip { get; set; }

        public bool IsError { get; set; }

        public decimal ResponseSpan { get; set; }
    }
}
