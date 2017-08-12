using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Api.FilterAttributes
{
    /// <summary>
    /// 参数类型约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class ValidateParmeterTypeOfAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public ValidateParmeterTypeOfAttribute(string name, Type type)
        {
            this.Type = type;
            this.Name = name;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            object value = new object();
            filterContext.ActionArguments.TryGetValue(Name, out value);

            if (value == null || value.GetType() != Type)
            {
                ResponseResult result = new ResponseResult();
                result.FunnctionName = filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString();
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.ErrorMsg = string.Format("参数错误。{0} 参数类型不正确。", Name);
                filterContext.Result = new JsonResult(result);
            }
        }
    }
}
