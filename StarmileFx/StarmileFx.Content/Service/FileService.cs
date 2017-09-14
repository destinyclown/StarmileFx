using StarmileFx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarmileFx.Content.Service
{
    public class FileService
    {
        public abstract class FileHelper
        {
            /// <summary>
            /// 是否成功
            /// </summary>
            /// <param name="isSuccess"></param>
            /// <param name="msg"></param>
            /// <param name="data"></param>
            /// <param name="httpCode"></param>
            /// <returns></returns>
            public static object IsSuccessMsg(bool isSuccess, string msg, dynamic data, HttpStatusCode httpCode = HttpStatusCode.OK)
            {
                return new { isSuccess = isSuccess, msg = msg, httpCode = httpCode, data = data };
            }
            /// <summary>
            /// 成功信息
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="data"></param>
            /// <param name="httpCode"></param>
            /// <returns></returns>
            public static object SuccessMsg(string msg, dynamic data, HttpStatusCode httpCode = HttpStatusCode.OK)
            {
                Result result = new Result
                {
                    IsSuccess = true
                };
                foreach (var a in data)
                {
                    result.ParamList.Add("", a);
                }
                result.ReasonDescription = msg;
                result.Reason = httpCode;
                return result;
            }
            /// <summary>
            /// 失败信息
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="httpCode"></param>
            /// <returns></returns>
            public static object ErrorMsg(string msg, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
            {
                Result result = new Result
                {
                    IsSuccess = false,
                    ReasonDescription = msg,
                    Reason = httpCode
                };
                return result;
            }
        }
    }
}
