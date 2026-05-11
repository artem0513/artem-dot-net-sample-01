using Sample01.Application.Core.Services;
using NLog.Web;

namespace Sample01.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        public NLog.Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public void LogError(string errorMessage)
        {
            logger.Error(errorMessage);
        }

        public void LogError(string errorMessage, params object[] args)
        {
            logger.Error(errorMessage, args);
        }

        public void LogException(Exception ex)
        {
            logger.Error(ex);
        }

        public void LogInfo(string infoMessage)
        {
            logger.Info(infoMessage);
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
            logger.Info(infoMessage, args);
        }
    }
}
