namespace MiniAccounting.Infrastructure.DataKeepers;

public class ReadWriteHistoryOfTransactionsFromFile : IReadWriteHistoryOfTransactions
{
    private const string PATH_TO_FILE = "historyOperations.json";
    private ILogger _logger;

    public ReadWriteHistoryOfTransactionsFromFile(ILogger logger)
    {
        _logger = new PrefixLogger(logger, "[HistoryOfTransactionsFromFile] ");
    }

    public List<TransactionInfo> ReadTransactions()
    {
        _logger.Trace("Read Transactions");

        if (!File.Exists(PATH_TO_FILE))
            return new List<TransactionInfo>();

        using (var reader = new StreamReader(PATH_TO_FILE, Static.Encoding))
        {
            var text = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(text))
                return new List<TransactionInfo>();

            return JsonConvert.DeserializeObject<List<TransactionInfo>>(text);
        }
    }

    public void WriteTransaction(TransactionInfo transactionInfo)
    {
        _logger.WriteLine($"Write Transaction: {transactionInfo}");
        var newList = ReadTransactions();

        using (var writer = new StreamWriter(PATH_TO_FILE, false))
        {
            newList.Add(transactionInfo);
            writer.WriteLine(JsonConvert.SerializeObject(newList, Formatting.Indented));
        }
    }
}
