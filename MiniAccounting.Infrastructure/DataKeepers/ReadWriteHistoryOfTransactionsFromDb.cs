namespace MiniAccounting.Infrastructure.DataKeepers
{
    public class ReadWriteHistoryOfTransactionsFromDb : IReadWriteHistoryOfTransactions
    {
        private ILogger _logger;
        private readonly MiniAccountingContext _miniAccountingContext;

        public ReadWriteHistoryOfTransactionsFromDb(ILogger logger, MiniAccountingContext context)
        {
            _miniAccountingContext = context;
            _logger = new PrefixLogger(logger, $"[{nameof(ReadWriteHistoryOfTransactionsFromDb)}] ");
        }

        public List<TransactionInfo> ReadTransactions()
        {
            _logger.Trace("Read Transactions");
            return _miniAccountingContext.Transactions.ToList();
        }

        public void WriteTransaction(TransactionInfo transactionInfo)
        {
            _logger.WriteLine($"Write Transaction: {transactionInfo}");
            _miniAccountingContext.Transactions.Add(transactionInfo);
            _miniAccountingContext.SaveChanges();
        }
    }
}