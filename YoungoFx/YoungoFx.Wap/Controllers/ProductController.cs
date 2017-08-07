using System.Threading.Tasks;
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


        public async Task<IActionResult> ProductList(string keyword)
        {
            ViewBag.Title = "产品列表";
            CacheProductList ProductList = await _YoungoServer.GetCacheProductList();
            List<ProductModel> list = ProductList.ProductList.Where(a => a.CnName.Contains(keyword) || a.Brand.Contains(keyword) || a.BrandIntroduce.Contains(keyword)).ToList();
            return View(list);
        }

        public IActionResult ProductList2(string keyword)
        {
            ViewBag.Title = "产品列表";
            return View();
        }
    }
}
