using NLog;

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
            logger.Warn("<span class='orange'>" + message + "</span>");
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            logger.Error("<span class='red'>" + message + "</span>");
        }
    }
}
