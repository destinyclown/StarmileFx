using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarmileFx.Common;
using static StarmileFx.Models.Enum.BaseEnum;

namespace StarmileFx.Api.FilterAttributes
{
    /// <summary>
    /// 参数类型约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class ValidateParmeterTypeOfAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ValidateParmeterTypeOfAttribute(string name, Type type)
        {
            this.Type = type;
            this.Name = name;
        }

        /// <summary>
        /// 参数类型验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            object value = new object();
            filterContext.ActionArguments.TryGetValue(Name, out value);

            if (value == null || value.GetType() != Type)
            {
                ResponseResult result = new ResponseResult
                {
                    IsSuccess = false,
                    Error = new Error
                    {
                        Code = ErrorCode.ParameterError,
                        Message = string.Format("{0} 参数类型不正确。", Name)
                    },
                };
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = new JsonResult(result);
            }
        }
    }
}
