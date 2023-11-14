using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace MiniAccounting.Tests;

[TestClass]
public class MiniAccountingServiceTests
{
    private ILogger _logger;
    private MiniAccountingClient _client;
    private HttpClient _httpClient;

    [TestInitialize]
    public void TestInitialize()
    {
        var consoleLogger = ConsoleLogger.Instance;
        consoleLogger.NeedWriteFullDate = false;
        _logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
        _logger.LogLevel = LogLevel.Trace;

        _httpClient = new HttpClient();
        _client = new MiniAccountingClient(_logger, _httpClient, "http://localhost:5099/");
    }

    [TestCleanup]
    public void Cleanup()
    {
        try
        {
            _httpClient?.Dispose();
        }
        catch (Exception ex)
        {
            try
            {
                _logger?.Error($"Ошибка во время TestCleanup: {ex}");
            }
            catch
            {
                Console.WriteLine($"Ошибка во время TestCleanup: {ex}");
            }
        }
    }

    [TestMethod]
    public async Task TopUpTotalBalanceTest()
    {
        //Arrange
        var addMoney = 50.0;
        var comment = "money";

        //Act
        var before = await _client.GetTotalBalanceAsync();
        _logger.WriteLine($"GetTotalBalanceAsync before = {before}");

        var result = await _client.TopUpTotalBalanceAsync(addMoney, comment);
        _logger.WriteLine($"TopUpTotalBalanceAsync result = {result}");

        var after = await _client.GetTotalBalanceAsync();
        _logger.WriteLine($"GetTotalBalanceAsync after = {after}");

        //Assert
        var expectedResult = before + addMoney;
        Assert.AreEqual(expectedResult, result, $"Значения результата не совпадает, ожидается {expectedResult}, получено {result}");
        Assert.AreEqual(expectedResult, after, $"Значения результата не совпадает, ожидается {expectedResult}, получено {after}");
    }

    [TestMethod]
    public async Task RevomeFromTotalBalanceTest()
    {
        var removeMoney = 101;
        var comment = "test 101";

        var before = await _client.GetTotalBalanceAsync();
        _logger.WriteLine($"GetTotalBalanceAsync before = {before}");

        var result = await _client.RemoveFromTotalBalanceAsync(removeMoney, comment);
        _logger.WriteLine($"RemoveFromTotalBalanceAsync result = {result}");

        var after = await _client.GetTotalBalanceAsync();
        _logger.WriteLine($"GetTotalBalanceAsync after = {result}");

        var expectedResult = before - removeMoney;
        Assert.AreEqual(expectedResult, result, $"Значения результата не совпадает, ожидается {expectedResult}, получено {result}");
        Assert.AreEqual(expectedResult, after, $"Значения результата не совпадает, ожидается {expectedResult}, получено {after}");
    }
    [TestMethod]
    //public async Task AddUserTest()
    //{

    //}
}