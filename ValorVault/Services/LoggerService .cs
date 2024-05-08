using Serilog;

using ILogger = Serilog.ILogger;

namespace ValorVault.Services.LoggerService
{
    public interface ILoggerService
    {
        void LogInfo(string infoMsg);
        void LogWarn(string warnMsg);
        void LogError(string errorMsg);
    }

    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInfo(string infoMsg)
        {
            _logger.Information(infoMsg);
        }

        public void LogWarn(string warnMsg)
        {
            _logger.Warning(warnMsg);
        }

        public void LogError(string errorMsg)
        {
            _logger.Error(errorMsg);
        }
    }
}
