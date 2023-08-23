using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniAccountingConsole.Core
{
    public class TransactionInfo
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public TypeOfTransaction TypeOfTransaction { get; set; }
        public string Comment { get; set; }
        public Guid UserUid { get; set; }
        public Guid DestinationUserUid { get; set; }

        public TransactionInfo(DateTimeOffset dateTimeOffset, TypeOfTransaction typeOfTransaction, string comment, Guid userUid, Guid destinationUserUid)
        {
            DateTimeOffset = dateTimeOffset;
            TypeOfTransaction = typeOfTransaction;
            Comment = comment;
            UserUid = userUid;
            DestinationUserUid = destinationUserUid;
        }
    }
}
