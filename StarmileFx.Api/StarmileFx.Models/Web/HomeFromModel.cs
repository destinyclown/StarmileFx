using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Web
{
    public class HomeFromModel
    {
        public class LoginFrom
        {
            public string loginName { get; set; }
            public string password { get; set; }
            public string validCode { get; set; }
            public string ip { get; set; }
        }
    }
}
