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

        public List<ActionHistory> ReadOperations()
        {
            if (!File.Exists(PATH_TO_FILE))
                return new List<ActionHistory>();

            using (var reader = new StreamReader(PATH_TO_FILE, _encoding))
            {
                var text = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(text))
                    return new List<ActionHistory>();

                return JsonConvert.DeserializeObject<List<ActionHistory>>(text);
            }
        }

        public void WriteOperation(ActionHistory actionHistory)
        {
            var newList = ReadOperations();

            using (var writer = new StreamWriter(PATH_TO_FILE, false))
            {
                newList.Add(actionHistory);
                writer.WriteLine(JsonConvert.SerializeObject(newList, Formatting.Indented));
            }
        }
    }
}
