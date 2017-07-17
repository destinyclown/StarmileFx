﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Models.Wap
{
    /// <summary>
    /// 搜索商品实体
    /// </summary>
    public class SearchProduct : ModelBase
    {
        /// <summary>
        /// 商品类型
        /// </summary>
        public ProductType ProductType { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Product> ProductList { get; set; }
    }
}