namespace StarmileFx.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SysMessage : ModelBase
    {
        public int Sender { get; set; }
        public int Receiver { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
    }
}
