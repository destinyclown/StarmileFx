using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Enum;

namespace StarmileFx.Models.Youngo
{
    public class Resources : ModelBase
    {
        /// <summary>
        /// 商品ID（SKU）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 资源标识
        /// </summary>
        public string ResourcesCode { get; set; }
        /// <summary>
        /// 路径地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public ResourcesEnum Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
