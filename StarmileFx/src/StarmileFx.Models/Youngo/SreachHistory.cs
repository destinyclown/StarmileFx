using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 商品
    /// </summary>
    public class SreachHistory : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? CustomerID { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
