using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            ErrorMsg = "请求失败，请检查API接口是否正常！";
            SendDateTime = DateTime.Now;
        }
        /// <summary>
        /// 请求方法
        /// </summary>
        public string FunnctionName { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// Token令牌（临时存在7天）
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendDateTime { get; set; }
    }

    /// <summary>
    /// Api相应实体
    /// </summary>
    public class ResponseResult<T> where T : new()
    {
        /// <summary>
        /// 请求方法
        /// </summary>
        public string FunnctionName { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// Token令牌（临时存在7天）
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendDateTime { get; set; }
    }
}
