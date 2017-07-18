﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Models.Wap
{
    /// <summary>
    /// 缓存商品列表
    /// </summary>
    public class CacheProductList
    {
        /// <summary>
        /// 商品类型列表
        /// </summary>
        public List<ProductType> ProductTypeList { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Product> ProductList { get ; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        public List<CustomerComment> CommentList { get; set; }
    }
}
