using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniAccountingConsole.Core
{
    public class ReadWriteHistoryOperationsFile : IReadWriteHistoryOperations
    {
        private const string PATH_TO_FILE = "historyOperations.json";
        private static readonly Encoding _encoding = Encoding.UTF8;

        public List<TransactionInfo> ReadOperations()
        {
            if (!File.Exists(PATH_TO_FILE))
                return new List<TransactionInfo>();

            using (var reader = new StreamReader(PATH_TO_FILE, _encoding))
            {
                var text = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(text))
                    return new List<TransactionInfo>();

                return JsonConvert.DeserializeObject<List<TransactionInfo>>(text);
            }
        }

        public void WriteOperation(TransactionInfo transactionInfo)
        {
            var newList = ReadOperations();

            using (var writer = new StreamWriter(PATH_TO_FILE, false))
            {
                newList.Add(transactionInfo);
                writer.WriteLine(JsonConvert.SerializeObject(newList, Formatting.Indented));
            }
        }
    }
}
