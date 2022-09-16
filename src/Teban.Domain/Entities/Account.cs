using Teban.Domain.Base;
using Teban.Domain.Enums;

namespace Teban.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal StartingBalance { get; set; }
        public AccountType AccountType { get; set; }

        public int BudgetId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ICollection<AccountTransaction>? AccountTransactions { get; set; }
    }
}
