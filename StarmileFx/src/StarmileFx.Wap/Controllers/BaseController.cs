using StarmileFx.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using StarmileFx.Models;

namespace StarmileFx.Wap.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 返回结果对象
        /// </summary>
        public Result result = new Result();
    }
}
