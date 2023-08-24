using System;

namespace MiniAccountingConsole.Logger
{
    public interface ILogger
    {
        LogLevel LogLevel { get; set; }
        void Write(LogLevel logLevel, string msg);
        void Error(Exception exception);
    }
}
