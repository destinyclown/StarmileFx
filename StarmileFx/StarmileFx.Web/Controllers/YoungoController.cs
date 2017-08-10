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
    public class YoungoController : Controller
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
            search.PageIndex = search.PageIndex == 0 ? 1 : search.PageIndex;
            ResponseResult<List<Product>> responseResult = await _YoungoServer.GetProductList(search);
            if (responseResult.IsSuccess)
            {
                var data = from m in responseResult.Content
                           select new
                           {
                               id = m.ProductID,
                               ProductID = m.ProductID,
                               Name = m.CnName,
                               Type = m.Type,
                               PurchasePrice = m.PurchasePrice,
                               SalesVolumem = m.SalesVolume,
                               State = m.State ? "有效" : "无效"
                           };
                total = int.Parse(responseResult.total.ToString());
                return Json(new { rows = data, total = total });
            }
            return Json(new { rows = list, total = total });
        }

        public IActionResult Suggest()
        {
            return View();
        }
    }
}
