using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniAccountingConsole.Core
{
    internal class ActionHistory
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public TypeOperation TypeOperation { get; set; }
        public string Comment { get; set; }
        public Guid UserUid { get; set; }
        public Guid DestinationUserUid { get; set; }

        public ActionHistory(DateTimeOffset dateTimeOffset, TypeOperation typeOperation, string comment, Guid userUid, Guid destinationUserUid)
        {
            DateTimeOffset = dateTimeOffset;
            TypeOperation = typeOperation;
            Comment = comment;
            UserUid = userUid;
            DestinationUserUid = destinationUserUid;
        }
    }
}
