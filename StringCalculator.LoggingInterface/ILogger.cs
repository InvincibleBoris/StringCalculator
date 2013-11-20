using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.LoggingInterface
{
    public enum LogLevel
    {
        FATAL = 0,
        ERROR = 1,
        WARN = 2,
        INFO = 3,
        VERBOSE = 4
    }

    public interface ILogger
    {
        void WriteMessage(string category, LogLevel level, string message);
    }
}
