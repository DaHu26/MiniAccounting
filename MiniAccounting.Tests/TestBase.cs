namespace MiniAccounting.Tests
{
    public class TestBase
    {
        protected ILogger Logger;
        protected MiniAccountingClient Client;
        protected HttpClient HttpClient;

        [TestInitialize]
        public void TestInitialize()
        {
            var consoleLogger = ConsoleLogger.Instance;
            consoleLogger.NeedWriteFullDate = false;
            Logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
            Logger.LogLevel = LogLevel.Trace;

            HttpClient = new HttpClient();
            Client = new MiniAccountingClient(Logger, HttpClient, "http://localhost:5099/");
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                HttpClient?.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    Logger?.Error($"Ошибка во время TestCleanup: {ex}");
                }
                catch
                {
                    Console.WriteLine($"Ошибка во время TestCleanup: {ex}");
                }
            }
        }

        public async Task Test(Func<Task> testAction, Func<bool, Task> removeSmthAction)
        {
            var isSuccess = true;

            try
            {
                await testAction();
            }
            catch
            {
                // записать в лог ошибку
                isSuccess = false;
                throw;
            }
            finally
            {
                try
                {
                    await removeSmthAction(isSuccess);
                }
                catch
                {
                    if (isSuccess)
                    {
                        throw;
                    }
                    else
                    {
                        // записать в лог ошибку
                    }
                }
            }
        }
    }
}
