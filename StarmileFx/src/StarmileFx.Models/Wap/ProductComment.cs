using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Wap
{
    public class ProductComment : ModelBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public int UserName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 商品ID（SKU）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 回复ID
        /// </summary>
        public int? Reply { get; set; }
    }
}
