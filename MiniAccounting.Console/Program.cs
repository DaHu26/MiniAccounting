using CommandLine;
using MiniAccounting.UI.Console;

ILogger logger = CreateLogger();

var cmdArgs = ParseCmdArgs(args, logger);

logger.LogLevel = (LogLevel)(int)cmdArgs.LogLevel;
try
{
    using var httpClient = new HttpClient();
    var client = new MiniAccountingClient(logger, httpClient, cmdArgs.MiniAccountingServiceAddress);
    var menu = new MainMenu(logger, client);
    await menu.StartAsync();
}
catch (Exception ex)
{
    logger.Error($"Unhandled exception: {ex}");
    throw;
}

static CmdArgs ParseCmdArgs(string[] args, ILogger logger)
{
    var cmdParserResult = Parser.Default.ParseArguments<CmdArgs>(args);

    if (cmdParserResult.Errors.Any())
    {
        foreach (var error in cmdParserResult.Errors)
        {
            logger.Error($"Ошибка во время парсинга опции коммандной строки: {error}");
        }
        throw new ArgumentException("Ошибка во время парсинга опций. См. лог.");
    }
    return cmdParserResult.Value;
}

static ILogger CreateLogger()
{
    ILogger logger;
    var consoleLogger = ConsoleLogger.Instance;
    consoleLogger.NeedWriteFullDate = false;
    logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
    logger.LogLevel = LogLevel.Trace;
    return logger;
}