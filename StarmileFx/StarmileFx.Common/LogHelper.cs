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
            logger.Info(message);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(ResponseResult result)
        {
            //logger.Error(@"请求地址：" + result.FunnctionName + "错误信息：" + result.ErrorMsg);
        }
    }
}
