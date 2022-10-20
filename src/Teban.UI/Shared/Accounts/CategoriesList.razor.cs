using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;
using Teban.Domain.Enums;
using Teban.UI.Services;

namespace Teban.UI.Shared.Accounts
{
    [Authorize]
    public partial class CategoriesList
    {
        [Inject]
        public MonthlyCategoryBudgetsService MonthlyCategoryBudgetsService { get; set; }

        [Parameter]
        public List<Account> Accounts { get; set; }
        [Parameter]
        public List<AccountTransaction> Transactions { get; set; }
        [Parameter]
        public DateTime MonthYear { get; set; }
        [Parameter]
        public string UserId { get; set; }

        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }

        protected override void OnParametersSet()
        {
            StartDate = new DateTime(MonthYear.Year, MonthYear.Month, 1).ToUniversalTime();
            EndDate = StartDate.AddMonths(1).ToUniversalTime();
        }

        private decimal GetNetSpent()
        {
            if (Transactions.Any(t => t.TransactionDate.ToUniversalTime().Date >= StartDate.Date
                && t.TransactionDate.ToUniversalTime().Date < EndDate))
            {
                return Accounts.Where(a => a.AccountType == AccountType.Category)
                    .Sum(a => a.GetAccountBalance(
                        Transactions.Where(t => t.TransactionEntries.Any(te => te.AccountId == a.AccountId))
                        .ToList(),
                        StartDate,
                        EndDate));
            }

            return 0M;
        }

        private decimal GetNetRemaining()
        {
            if (Transactions.Any(t => t.TransactionDate.ToUniversalTime().Date >= StartDate.Date
                && t.TransactionDate.ToUniversalTime().Date < EndDate))
            {
                return Accounts.Where(a => a.AccountType == AccountType.Category)
                    .Sum(a => a.GetRemainingBalance(
                        Transactions.Where(t => t.TransactionEntries.Any(te => te.AccountId == a.AccountId))
                        .ToList(),
                        StartDate,
                        EndDate));
            }

            return 0M;
        }

        private decimal GetNetBudgeted()
        {
            if (Accounts.Any(a => a.MonthlyCategoryBudgets
                    .Any(m => m.MonthYear.Month == StartDate.Month
                        && m.MonthYear.Year == StartDate.Year)))
            {
                return Accounts.Where(a => a.AccountType == AccountType.Category)
                    .Sum(a => a.GetBudgetedBalance(StartDate));
            }

            return 0M;
        }

        private async Task MonthlyCategoryBudgetSubmit(MonthlyCategoryBudget monthlyCategoryBudget)
        {
            if (monthlyCategoryBudget.MonthlyCategoryBudgetId > 0)
            {
                var result = await MonthlyCategoryBudgetsService.PutMonthlyCategoryBudget(monthlyCategoryBudget.MonthlyCategoryBudgetId, monthlyCategoryBudget);

                if (result.Succeeded)
                {
                    var accountIndex = Accounts.FindIndex(a => a.AccountId == monthlyCategoryBudget.AccountId);
                    var monthlyCategoryBudgetIndex = Accounts[accountIndex].MonthlyCategoryBudgets
                        .FindIndex(m => m.MonthlyCategoryBudgetId == monthlyCategoryBudget.MonthlyCategoryBudgetId);
                    Accounts[accountIndex].MonthlyCategoryBudgets[monthlyCategoryBudgetIndex] = monthlyCategoryBudget;
                }
            }
            else
            {
                var result = await MonthlyCategoryBudgetsService.PostMonthlyCategoryBudget(monthlyCategoryBudget);

                if (result.Succeeded)
                {
                    monthlyCategoryBudget = result.Data;
                    Accounts.First(a => a.AccountId == monthlyCategoryBudget.AccountId)
                        .MonthlyCategoryBudgets.Add(monthlyCategoryBudget);
                }
            }
        }
    }
}
