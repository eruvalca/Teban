using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class TransactionEntry : AuditableEntity
    {
        public int TransactionEntryId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }

        public int AccountTransactionId { get; set; }
        public int AccountId { get; set; }
    }
}
