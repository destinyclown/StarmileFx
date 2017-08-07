﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Wap;
using YoungoFx.Web.Server.IService;
using StarmileFx.Models;
using StarmileFx.Models.MongoDB;

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
            ProductModel product = ProductList.ProductList.Find(a => a.ProductID == productid);
            //List<Resources> resources = ProductList.ResourcesList == null ? new List<Resources>() : ProductList.ResourcesList.Where(a => a.ResourcesCode == productid).ToList();
            //List<ProductComment> Comment = ProductList.CommentList == null ? new List<ProductComment>() : ProductList.CommentList.Where(a => a.ProductID == productid).ToList();
            ResponseResult<ProductResources> responseResult = await _YoungoServer.GetProductResources(productid);
            ProductResources resources = new ProductResources();
            if (responseResult.IsSuccess)
            {
                resources = responseResult.Content;
            }
            _product.ProductID = productid;
            _product.Name = product.CnName;
            _product.PurchasePrice = product.PurchasePrice;
            _product.Introduce = product.Introduce;
            _product.Type = product.Type;
            _product.SalesVolume = product.SalesVolume;
            _product.Explain = ProductList.ExpressList.Find(a => a.ExpressCode == product.ExpressCode).Explain;
            _product.Remarks = product.Remarks;
            _product.CostPrice = product.CostPrice;
            _product.ResourcesList = resources.ResourcesList;
            _product.CommentList = resources.CommentList;
            _product.Brand = product.Brand;
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
