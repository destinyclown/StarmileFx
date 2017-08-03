using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Web.Controllers
{
    public class YoungoController : Controller
    {
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
        public IActionResult Suggest()
        {
            return View();
        }
    }
}
