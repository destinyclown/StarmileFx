using NLog;
using StarmileFx.Models;

namespace StarmileFx.Common
{
    /// <summary>
    /// 日志文件类
    /// </summary>
    public static class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static LogHelper()
        {
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            logger.Info(message+ "<br/>");
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            logger.Warn("<span  style='color: orange'>" + message + "</span><br/>");
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(ResponseResult result)
        {
            logger.Error(@"<br/>请求地址：<span style='color: red'> " + result.FunnctionName + "</span><br/>错误信息：<span style='color: red'>" + result.ErrorMsg + "</span><br/>");
        }
    }
}
