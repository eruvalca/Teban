using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class AccountTransaction : AuditableEntity
    {
        public int AccountTransactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Payee { get; set; }

        public int BudgetId { get; set; }
        public int AccountId { get; set; }
        public Category? Category { get; set; }
        public ICollection<TransactionEntry>? TransactionEntries { get; set; }
    }
}
