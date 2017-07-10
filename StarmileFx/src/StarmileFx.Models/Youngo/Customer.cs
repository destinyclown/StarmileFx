using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Customer : ModelBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 微信Key
        /// </summary>
        public string WeCharKey { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 用户类型（1、普通用户2、白金会员3、黄金会员）
        /// </summary>
        public CustomerTypeEnum CustomerType { get; set; }
    }
}
