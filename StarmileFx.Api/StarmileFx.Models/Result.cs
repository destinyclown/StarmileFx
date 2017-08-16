using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarmileFx.Models
{
    /// <summary>
    /// 返回值模型
    /// </summary>
    public class Result : ModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Result()
        {
            IsSuccessful = false;
            Reason = HttpStatusCode.OK;
            ReasonDescription = "";
            ParamList = new Dictionary<string, object>();
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 原因，数值
        /// </summary>
        public HttpStatusCode Reason { get; set; }

        /// <summary>
        /// 原因描述
        /// </summary>
        public string ReasonDescription { get; set; }

        /// <summary>
        /// 自定义返回参数列表
        /// </summary>
        public Dictionary<string, object> ParamList { get; set; }

        /// <summary>
        /// 获取原因描述
        /// </summary>
        //public string GetReasonDescription(Type ReasonType)
        //{
        //    if (ReasonType != null)
        //    {
        //        return BaseTool.GetEnumTextById(ReasonType, Reason);
        //    }
        //    return "";
        //}
    }
}
