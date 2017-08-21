using NLog;

namespace StarmileFx.Common
{
    public static class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static LogHelper()
        {
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }
    }
}
