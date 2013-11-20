using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.LoggingInterface;

namespace StringCalculator.Log4Net
{
    using System.Security.Cryptography.X509Certificates;

    using log4net;

    internal class Log4NetLogger : ILogger
    {
        public Log4NetLogger()
        {
            // Configures Log4Net by using the XMLConfiguration class of Log4Net
            log4net.Config.XmlConfigurator.Configure();
        }

        public void WriteMessage(string category, LogLevel level, string message)
        {
            ILog log = LogManager.GetLogger(category);

            switch (level)
            {
                case LogLevel.FATAL:
                    if (log.IsFatalEnabled) log.Fatal(message);
                    break;
                case LogLevel.ERROR:
                    if (log.IsErrorEnabled) log.Error(message);
                    break;
                case LogLevel.WARN:
                    if (log.IsWarnEnabled) log.Warn(message);
                    break;
                case LogLevel.INFO:
                    if (log.IsInfoEnabled) log.Info(message);
                    break;
                case LogLevel.VERBOSE:
                    if (log.IsDebugEnabled) log.Debug(message);
                    break;
            }
        }

    }
}
