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

        public decimal GetAccountBalance(IEnumerable<TransactionEntry> transactionEntries)
        {
            var balance = StartingBalance;

            balance += transactionEntries.Sum(a => a.Amount);

            return balance;
        }
    }
}
