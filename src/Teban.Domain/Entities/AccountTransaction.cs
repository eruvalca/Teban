using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class AccountTransaction : AuditableEntity
    {
        public int AccountTransactionId { get; set; }
        public string Description { get; set; } = string.Empty;

        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public ICollection<TransactionEntry>? TransactionEntries { get; set; }
    }
}
