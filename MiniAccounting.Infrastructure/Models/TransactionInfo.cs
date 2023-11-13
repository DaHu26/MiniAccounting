using System.ComponentModel.DataAnnotations;

namespace MiniAccounting.Infrastructure
{
    public class TransactionInfo
    {
        [Key]
        public Guid Uid { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
        public TypeOfTransaction TypeOfTransaction { get; set; }
        public string Comment { get; set; }
        public Guid UserUid { get; set; }
        public Guid DestinationUserUid { get; set; }

        public TransactionInfo(Guid uid, DateTimeOffset dateTimeOffset, TypeOfTransaction typeOfTransaction, string comment, Guid userUid, Guid destinationUserUid)
        {
            Uid = uid;
            DateTimeOffset = dateTimeOffset;
            TypeOfTransaction = typeOfTransaction;
            Comment = comment;
            UserUid = userUid;
            DestinationUserUid = destinationUserUid;
        }

        public override string ToString()
        {
            return Static.ToStringPropsWithReflection(this);
        }
    }
}
