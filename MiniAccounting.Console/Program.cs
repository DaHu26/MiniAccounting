ILogger logger;

var consoleLogger = ConsoleLogger.Instance;
consoleLogger.NeedWriteFullDate = false;
logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
logger.LogLevel = LogLevel.Trace;
try
{
    var menu = new MainMenu(logger);
    menu.Start();
}
catch (Exception ex)
{
    logger.Error($"Unhandled exception: {ex}");
    throw;
}