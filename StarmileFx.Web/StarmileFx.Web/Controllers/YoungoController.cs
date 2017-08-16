using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models.Web;
using StarmileFx.Web.Server.IService;
using StarmileFx.Models.Youngo;
using StarmileFx.Models;

namespace StarmileFx.Web.Controllers
{
    public class YoungoController : BaseController
    {
        private readonly IYoungoServer _YoungoServer;

        public YoungoController(IYoungoServer IYoungoServer)
        {
            _YoungoServer = IYoungoServer;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Express()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }

        public async Task<IActionResult> GetProductList(ProductSearch search)
        {
            List<Product> list = new List<Product>();
            int total = 0;
            search.PageIndex = search.PageIndex / search.PageSize + 1;
            ResponseResult<List<ProductWeb>> responseResult = await _YoungoServer.GetProductList(search);
            if (responseResult.IsSuccess)
            {
                var data = from m in responseResult.Content
                           select new
                           {
                               id = m.ID,
                               productId = m.ProductID,
                               name = m.CnName,
                               type = m.TypeName,
                               picture = m.Picture,
                               purchasePrice = m.PurchasePrice.ToString("F2") + "元",
                               salesVolumem = m.SalesVolume,
                               isTop = m.IsTop ? "<i class='fa fa-ok fa-fw'></i>" : "<i class='fa fa-ban fa-fw'></i>",
                               state = m.State ? "有效" : "无效",
                               isClearStock = m.IsClearStock ? "<i class='fa fa-ok fa-fw'></i>" : "<i class='fa fa-ban fa-fw'></i>",
                               creatTime = m.CreatTime.ToString("yyyy-MM-dd hh:mm"),
                           };
                total = int.Parse(responseResult.total.ToString());
                return Json(new { rows = data, total = total });
            }
            return Json(new { rows = list, total = total });
        }

        public async Task<IActionResult> DeleteProduct(int Id)
        {
            ResponseResult<bool> responseResult = await _YoungoServer.DeleteProduct(Id);
            if (responseResult.IsSuccess && responseResult.Content)
            {
                result.IsSuccessful = responseResult.Content;
                return Json(result);
            }
            else
            {
                result.IsSuccessful = responseResult.Content;
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
        }
        public async Task<IActionResult> BatchDeleteProduct(int[] Ids)
        {
            ResponseResult<bool> responseResult = await _YoungoServer.BatchDeleteProduct(Ids);
            if (responseResult.IsSuccess && responseResult.Content)
            {
                result.IsSuccessful = responseResult.Content;
                return Json(result);
            }
            else
            {
                result.IsSuccessful = responseResult.Content;
                result.ReasonDescription = responseResult.ErrorMsg;
                return Json(result);
            }
        }

        public IActionResult Suggest()
        {
            return View();
        }
    }
}
