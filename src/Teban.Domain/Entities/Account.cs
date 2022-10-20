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

        public decimal GetAccountBalance(IEnumerable<AccountTransaction> transactions)
        {
            var balance = StartingBalance;

            foreach (var transaction in transactions)
            {
                if (transaction.TransactionEntries is not null && transaction.TransactionEntries.Any())
                {
                    balance += transaction.TransactionEntries
                        .Where(t => t.AccountId == AccountId)
                        .Sum(t => t.Amount);
                }
            }

            return balance;
        }

        public decimal GetAccountBalance(IEnumerable<AccountTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            var balance = StartingBalance;

            var filteredTransactions = transactions
                .Where(t => t.TransactionDate.ToUniversalTime().Date >= startDate.ToUniversalTime().Date
                    && t.TransactionDate.ToUniversalTime().Date < endDate.ToUniversalTime().Date);

            foreach (var transaction in filteredTransactions)
            {
                if (transaction.TransactionEntries is not null && transaction.TransactionEntries.Any())
                {
                    balance += transaction.TransactionEntries
                        .Where(t => t.AccountId == AccountId)
                        .Sum(t => t.Amount);
                }
            }

            return balance;
        }
    }
}
