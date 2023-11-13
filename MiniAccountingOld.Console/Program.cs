using MiniAccountingConsole.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole
{
    internal class Program
    {
        private static ILogger _logger;

        static void Main(string[] args)
        {
            var consoleLogger = ConsoleLogger.Instance;
            consoleLogger.NeedWriteFullDate = false;
            _logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
            _logger.LogLevel = LogLevel.Trace;
            try
            {
                var menu = new MainMenu(_logger);
                menu.Start();
            }
            catch (Exception ex)
            {
                _logger.Error($"Unhandled exception: {ex}");
                throw;
            }
        }
    }
}

