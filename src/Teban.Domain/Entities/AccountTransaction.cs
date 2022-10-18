using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class AccountTransaction : AuditableEntity
    {
        public int AccountTransactionId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Today;
        public string Description { get; set; } = string.Empty;
        public string? Payee { get; set; }
        public bool IsTransfer { get; set; }
        public bool IsInflow { get; set; }

        public int BudgetId { get; set; }
        public ICollection<TransactionEntry>? TransactionEntries { get; set; }
    }
}
