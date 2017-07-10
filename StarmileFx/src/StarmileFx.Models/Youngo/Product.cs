using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Youngo
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : ModelBase
    {
        /// <summary>
        /// 商品标识（即SKU,其他表记录的均为此字段）
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CnName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public float Weight { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public float CostPrice { get; set; }
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
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }
        /// <summary>
        /// 是否缺货
        /// </summary>
        public bool IsOutOfStock { get; set; }
        /// <summary>
        /// 是否清货
        /// </summary>
        public bool IsClearStock { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime? OnlineTime { get; set; }
    }
}
