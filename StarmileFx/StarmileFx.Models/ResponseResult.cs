using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static StarmileFx.Models.Enum.BaseEnum;

namespace StarmileFx.Models
{
    /// <summary>
    /// Api相应实体
    /// </summary>
    public class ResponseResult
    {
        public ResponseResult()
        {
            IsSuccess = false;
            Data = null;
            Error = null;
            SystemError = null;
            //ErrorMsg = string.Empty;
            //SendDateTime = DateTime.Now;
            //Total = 0;
            //Token = "";
        }
        /// <summary>
        /// 请求方法
        /// </summary>
        //public string FunnctionName { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        [Description("是否成功")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Description("内容")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Data { get; set; }

        /// <summary>
        /// 违背业务规则类异常
        /// </summary>
        [Description("违背业务规则类异常")]
        public Error Error { get; set; }

        /// <summary>
        /// 系统异常
        /// </summary>
        [Description("系统异常")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SystemError SystemError { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        //public string ErrorMsg { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        //public int Total { get; set; }
        /// <summary>
        /// Token令牌（临时存在7天）
        /// </summary>
        //public string Token { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        //public DateTime SendDateTime { get; set; }
    }

    /// <summary>
    /// Api相应实体
    /// </summary>
    public class ResponseResult<T> where T : new()
    {
        public ResponseResult()
        {
            IsSuccess = false;
            Data = default(T);
            Error = null;
            SystemError = null;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        /// <summary>
        /// 违背业务规则类异常
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// 系统异常
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SystemError SystemError { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        //public int? Total { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        //public string ErrorMsg { get; set; }
        /// <summary>
        /// Token令牌（临时存在7天）
        /// </summary>
        //public string Token { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        //public DateTime SendDateTime { get; set; }
    }

    /// <summary>
    /// 违背业务规则类异常
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 系统异常
    /// </summary>
    public class SystemError
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 帮助地址
        /// </summary>
        public string HelpUrl { get; set; }
    }
}
