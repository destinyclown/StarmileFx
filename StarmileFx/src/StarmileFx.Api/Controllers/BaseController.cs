using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarmileFx.Common;
using StarmileFx.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StarmileFx.Api.Controllers
{
    public class BaseController : Controller
    {
        public string ActionResponseGetString(Func<ResponseResult> action)
        {
            ResponseResult result = ActionResponse(action);
            result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
            try
            {
                result.IsSuccess = true;
                string json = JsonHelper.T_To_Json(result);
                return json;
            }
            catch (Exception ex)
            {
                result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
                string json = JsonHelper.T_To_Json(result);
                return json;
            }
        }

        public ResponseResult ActionResponse(Func<ResponseResult> action)
        {

            ResponseResult result = new ResponseResult();
            result.FunnctionName = RouteData.Values["controller"].ToString() + "/" + RouteData.Values["action"].ToString();
            try
            {
                result = action.Invoke();
                result.SendDateTime = DateTime.Now;
                if (!result.IsSuccess && !string.IsNullOrEmpty(result.ErrorMsg))
                {
                    result.ErrorMsg = result.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.SendDateTime = DateTime.Now;
                result.Content = "";
                result.ErrorMsg = ex.Message;
            }

            return result;
        }
    }
}
