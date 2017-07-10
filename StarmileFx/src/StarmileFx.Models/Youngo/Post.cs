using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 邮寄方式
    /// </summary>
    public class Post : ModelBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 邮寄标识
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 是否停止使用
        /// </summary>
        public bool IsStop { get; set; }
    }
}
