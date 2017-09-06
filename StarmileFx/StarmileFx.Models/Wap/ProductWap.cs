﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Models.Youngo;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarmileFx.Models.Wap
{
    /// <summary>
    /// 网站商品
    /// </summary>
    [NotMapped]
    public class ProductWap
    {
        /// <summary>
        /// 商品标识（即SKU,其他表记录的均为此字段）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public float CostPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public float PurchasePrice { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int SalesVolume { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 快递名称
        /// </summary>
        public string ExpressName { get; set; }
        /// <summary>
        /// 快递说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 品牌介绍
        /// </summary>
        public string BrandIntroduce { get; set; }
        /// <summary>
        /// 资源列表
        /// </summary>
        public List<Resources> ResourcesList { get; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        public List<ProductComment> CommentList { get; set; }
    }
}
