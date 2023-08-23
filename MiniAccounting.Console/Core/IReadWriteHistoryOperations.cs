using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    public interface IReadWriteHistoryOperations
    {
        /// <summary>
        /// Сохранение транзакции.
        /// </summary>
        /// <param name="actionHistory">Транзакция которую сохраняем.</param>
        void WriteOperation(ActionHistory actionHistory);

        /// <summary>
        /// Считываем историю транзакций.
        /// </summary>
        /// <returns>История транзакций. Если истории нет, то вернется пустой список.</returns>
        List<ActionHistory> ReadOperations();
    }
}
