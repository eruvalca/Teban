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
        public ICollection<AccountTransaction>? AccountTransactions { get; set; }

        public decimal GetAccountBalance()
        {
            var balance = StartingBalance;

            if (AccountTransactions is not null)
            {
                if (AccountTransactions.Any())
                {
                    foreach (var accountTransaction in AccountTransactions)
                    {
                        if (accountTransaction.TransactionEntries is not null)
                        {
                            if (accountTransaction.TransactionEntries.Any())
                            {
                                balance += accountTransaction.TransactionEntries.Sum(a => a.CreditAmount + a.DebitAmount);
                            }
                        }
                    }
                }
            }

            return balance;
        }
    }
}
