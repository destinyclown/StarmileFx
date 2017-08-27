using System.Collections.Generic;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;

namespace StarmileFx.Models.Redis
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
        public List<ProductModel> ProductList { get ; set; }
        /// <summary>
        /// 资源列表
        /// </summary>
        //public List<Resources> ResourcesList { get; set; }
        /// <summary>
        /// 评论列表
        /// </summary>
        //public List<ProductComment> CommentList { get; set; }
        /// <summary>
        /// 快递列表
        /// </summary>
        public List<Express> ExpressList { get; set; }
    }
}
