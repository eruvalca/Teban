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
        public List<MonthlyCategoryBudget>? MonthlyCategoryBudgets { get; set; }

        public decimal GetAccountBalance(List<AccountTransaction> transactions)
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

        public decimal GetAccountBalance(List<AccountTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            var balance = StartingBalance;

            var filteredTransactions = transactions
                .Where(t => t.TransactionDate.Date >= startDate.Date
                    && t.TransactionDate.Date < endDate.Date
                    && t.TransactionEntries.Any(te => te.AccountId == AccountId))
                .ToList();

            foreach (var transaction in filteredTransactions)
            {
                balance += transaction.TransactionEntries
                    .Where(t => t.AccountId == AccountId)
                    .Sum(t => t.Amount);
            }

            return balance;
        }

        public decimal GetRemainingBalance(List<AccountTransaction> transactions, DateTime startDate, DateTime endDate)
        {
            var balance = StartingBalance;
            var matchingMonthlyCategoryBudgetAmount = 0M;

            var filteredTransactions = transactions
                .Where(t => t.TransactionDate.Date >= startDate.Date
                    && t.TransactionDate.Date < endDate.Date
                    && t.TransactionEntries.Any(te => te.AccountId == AccountId))
                .ToList();

            foreach (var transaction in filteredTransactions)
            {
                balance += transaction.TransactionEntries
                    .Where(t => t.AccountId == AccountId)
                    .Sum(t => t.Amount);
            }

            if (MonthlyCategoryBudgets is not null && MonthlyCategoryBudgets.Any())
            {
                var matchingMonthlyCategoryBudget = MonthlyCategoryBudgets
                    .FirstOrDefault(m => m.MonthYear.Month == startDate.Month
                        && m.MonthYear.Year == startDate.Year
                        && m.AccountId == AccountId);

                if (matchingMonthlyCategoryBudget is not null)
                {
                    matchingMonthlyCategoryBudgetAmount = matchingMonthlyCategoryBudget.Amount;
                }
            }

            return matchingMonthlyCategoryBudgetAmount - balance;
        }

        public decimal GetBudgetedBalance(DateTime startDate)
        {
            if (MonthlyCategoryBudgets is not null && MonthlyCategoryBudgets.Any())
            {
                var matchingMonthlyCategoryBudget = MonthlyCategoryBudgets
                    .FirstOrDefault(m => m.MonthYear.Month == startDate.Month
                        && m.MonthYear.Year == startDate.Year
                        && m.AccountId == AccountId);

                if (matchingMonthlyCategoryBudget is not null)
                {
                    return matchingMonthlyCategoryBudget.Amount;
                }
            }

            return 0M;
        }
    }
}
