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

        private IReadWriteHistoryOperations _readWriteHistoryOperations;

        public Operator()
        {
            Users = new List<User>();
            _readWriteHistoryOperations = new ReadWriteHistoryOperationsFile();
        }

        public double TopUpTotalBalance(double addMoney, string comment)
        {
            var operationInfo = new TransactionInfo(DateTimeOffset.UtcNow, TypeOperation.TopUp, comment, Guid.Empty, Guid.Empty);
            _readWriteHistoryOperations.WriteOperation(operationInfo);

            return TotalMoney += addMoney;
        }

        public double RemoveFromTotalBalance(double removeMoney, string comment)
        {
            var operationInfo = new TransactionInfo(DateTimeOffset.UtcNow, TypeOperation.Remove, comment, Guid.Empty, Guid.Empty);
            _readWriteHistoryOperations.WriteOperation(operationInfo);

            return TotalMoney -= removeMoney;
        }
    }
}
