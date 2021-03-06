﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StarmileFx.Common;
using StarmileFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Api.FilterAttributes
{
    /// <summary>
    /// 空参数约束，多个参数以“逗号”隔开
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateParmeterNullAttribute : ActionFilterAttribute
    {
        public string Parmters { get; set; }
        public ValidateParmeterNullAttribute(string parmters)
        {
            this.Parmters = parmters;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            foreach (string item in Parmters.Split(','))
            {
                object value = new object();
                filterContext.ActionArguments.TryGetValue(item.Trim(), out value);
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    ResponseResult result = new ResponseResult
                    {
                        FunnctionName = filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString(),
                        IsSuccess = false,
                        SendDateTime = DateTime.Now,
                        ErrorMsg = string.Format("参数错误。{0} 参数不能为Null。", item.Trim())
                    };
                    filterContext.Result = new JsonResult(result);
                    //throw new Exception(result.ErrorMsg);
                    LogHelper.Error(result);
                }
            }
        }
    }
}
