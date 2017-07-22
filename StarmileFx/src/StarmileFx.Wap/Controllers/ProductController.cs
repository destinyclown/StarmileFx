using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Redis;
using StarmileFx.Models.Wap;
using StarmileFx.Models.Youngo;
using StarmileFx.Wap.Server.IService;

namespace StarmileFx.Wap.Controllers
{
    public class ProductController : BaseController
    {
        //依赖注入
        private readonly IYoungoServer _YoungoServer;
        public ProductController(IYoungoServer YoungoServer)
        {
            _YoungoServer = YoungoServer;
        }

        public async Task<IActionResult> Index()
        {
            CacheProductList ProductList = await _YoungoServer.GetCacheProductList();
            ViewBag.Title = "分类列表";
            return View(ProductList);
        }

        public async Task<IActionResult> Product(string productid)
        {
            CacheProductList ProductList = await _YoungoServer.GetCacheProductList();
            ProductWap _product = new ProductWap();
            Product product = ProductList.ProductList.Where(a => a.ProductID == productid).ToList()[0];
            List<Resources> resources = ProductList.ResourcesList.Where(a => a.ResourcesCode == productid).ToList();
            List<ProductComment> Comment = ProductList.CommentList.Where(a => a.ProductID == productid).ToList();
            _product.ProductID = productid;
            _product.Name = product.CnName;
            _product.PurchasePrice = product.PurchasePrice;
            _product.Type = product.Type;
            _product.Stock = product.Stock;
            _product.CostPrice = product.CostPrice;
            _product.ResourcesList = resources;
            _product.CommentList = Comment;
            ViewBag.Title = "产品详情";
            return View(_product);
        }

        public IActionResult ProductList()
        {
            ViewBag.Title = "产品列表";
            return View();
        }

        public IActionResult ProductList2()
        {
            ViewBag.Title = "产品列表";
            return View();
        }
    }
}
