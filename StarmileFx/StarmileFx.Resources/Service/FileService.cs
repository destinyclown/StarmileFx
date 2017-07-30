using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarmileFx.Resources.Service
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
            /// <param name="dataCount"></param>
            /// <param name="httpCode"></param>
            /// <returns></returns>
            public static object SuccessMsg(string msg, dynamic data = null, int dataCount = 0, HttpStatusCode httpCode = HttpStatusCode.OK)
            {
                return new { isSuccess = true, msg = msg, httpCode = httpCode, data = data, dataCount = dataCount };
            }
            /// <summary>
            /// 失败信息
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="errorCode"></param>
            /// <param name="errorLevel"></param>
            /// <param name="httpCode"></param>
            /// <returns></returns>
            public static object ErrorMsg(string msg, int errorCode = 0, int errorLevel = 0, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
            {
                return new { isSuccess = false, msg = msg, httpCode = httpCode, errorCode = errorCode, errorLevel = errorLevel };
            }
        }
    }
}
