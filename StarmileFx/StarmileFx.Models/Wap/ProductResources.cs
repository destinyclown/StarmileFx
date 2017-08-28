using StarmileFx.Models.Youngo;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarmileFx.Models.Wap
{
    /// <summary>
    /// 商品资源
    /// </summary>
    public class ProductResources
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        public List<ProductComment> CommentList { get; set; }
        /// <summary>
        /// 图片资源
        /// </summary>
        public List<Resources> ResourcesList { get; set; }
    }
}
