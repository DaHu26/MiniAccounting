using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    public interface IReadWriteHistoryOfTransactions
    {
        /// <summary>
        /// Сохранение транзакции.
        /// </summary>
        /// <param name="transactionInfo">Транзакция которую сохраняем.</param>
        void WriteTransaction(TransactionInfo transactionInfo);

        /// <summary>
        /// Считываем историю транзакций.
        /// </summary>
        /// <returns>История транзакций. Если истории нет, то вернется пустой список.</returns>
        List<TransactionInfo> ReadTransactions();
    }
}
