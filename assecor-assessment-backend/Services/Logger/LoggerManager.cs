using System;
using NLog;

namespace assecor_assessment_backend.Services.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
            var layout = "${longdate}|${level}|${message}${onexception:${newline}${exception:format=tostring}}";
            var configuration = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "${basedir}/logfile.log" };
            logfile.Layout = layout;

            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            logconsole.Layout = layout;

            configuration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logfile);
            configuration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logconsole);

            NLog.LogManager.Configuration = configuration;
        }
        
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogError(Exception exception)
        {
            logger.Error(exception);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}