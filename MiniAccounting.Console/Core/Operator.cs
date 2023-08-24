using MiniAccountingConsole.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    public class Operator
    {
        public double TotalMoney { get; private set; }
        public List<User> Users { get; private set; }

        private ILogger _logger;
        

        private IReadWriteHistoryOfTransactions _readWriteHistoryOfTransactions;

        public Operator(ILogger logger)
        {
            _logger = new PrefixLogger(logger, $"[{nameof(Operator)}] ");
            Users = new List<User>();
            _readWriteHistoryOfTransactions = new ReadWriteHistoryOfTransactionsFromFile(_logger);
        }

        public double TopUpTotalBalance(double addMoney, string comment)
        {
            _logger.WriteLine($"{nameof(TopUpTotalBalance)}: addmoney={addMoney}, comment={comment}");

            var operationInfo = new TransactionInfo(DateTimeOffset.UtcNow, TypeOfTransaction.TopUp, comment, Guid.Empty, Guid.Empty);
            _readWriteHistoryOfTransactions.WriteTransaction(operationInfo);

            return TotalMoney += addMoney;
        }

        public double RemoveFromTotalBalance(double removeMoney, string comment)
        {
            _logger.WriteLine($"{nameof(RemoveFromTotalBalance)}: removeMoney={removeMoney}, comment={comment}");

            var operationInfo = new TransactionInfo(DateTimeOffset.UtcNow, TypeOfTransaction.Remove, comment, Guid.Empty, Guid.Empty);
            _readWriteHistoryOfTransactions.WriteTransaction(operationInfo);

            return TotalMoney -= removeMoney;
        }
    }
}
