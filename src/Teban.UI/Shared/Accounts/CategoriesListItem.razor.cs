using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;
using Teban.UI.Services;

namespace Teban.UI.Shared.Accounts
{
    [Authorize]
    public partial class CategoriesListItem
    {
        [Parameter]
        public Account Account { get; set; }
        [Parameter]
        public List<AccountTransaction> Transactions { get; set; }
        [Parameter]
        public DateTime StartDate { get; set; }
        [Parameter]
        public DateTime EndDate { get; set; }
        [Parameter]
        public string UserId { get; set; }
        [Parameter]
        public EventCallback<MonthlyCategoryBudget> MonthlyCategoryBudgetSubmitCallback { get; set; }

        private decimal RemainingBalance { get; set; }
        private decimal AccountBalance { get; set; }
        private decimal BudgetedBalance { get; set; }
        private MonthlyCategoryBudget ThisMonthlyCategoryBudget { get; set; } = new();

        protected override void OnParametersSet()
        {
            RemainingBalance = Account
                .GetRemainingBalance(Transactions
                    .Where(t => t.TransactionEntries
                        .Any(te => te.AccountId == Account.AccountId))
                    .ToList(),
                    StartDate,
                    EndDate);

            AccountBalance = Account
                .GetAccountBalance(Transactions
                    .Where(t => t.TransactionEntries
                        .Any(te => te.AccountId == Account.AccountId))
                    .ToList(),
                    StartDate,
                    EndDate);

            BudgetedBalance = Account.GetBudgetedBalance(StartDate);

            ThisMonthlyCategoryBudget = Account.MonthlyCategoryBudgets
                .FirstOrDefault(m => m.MonthYear.ToUniversalTime().Month == StartDate.Month
                    && m.MonthYear.ToUniversalTime().Year == StartDate.Year);
        }

        private async Task HandleMonthlyCategoryBudgetSubmit(MonthlyCategoryBudget monthlyCategoryBudget, int accountId, ChangeEventArgs e)
        {
            if (monthlyCategoryBudget is null)
            {
                monthlyCategoryBudget = new MonthlyCategoryBudget
                {
                    MonthYear = new DateTime(StartDate.Year, StartDate.Month, 1).ToUniversalTime(),
                    Amount = decimal.Parse(e.Value.ToString()),
                    AccountId = accountId,
                    CreatedBy = UserId
                };

                await MonthlyCategoryBudgetSubmitCallback.InvokeAsync(monthlyCategoryBudget);
            }
            else
            {
                monthlyCategoryBudget.Amount = decimal.Parse(e.Value.ToString());
                monthlyCategoryBudget.Modified = DateTime.UtcNow;
                monthlyCategoryBudget.ModifiedBy = UserId;

                await MonthlyCategoryBudgetSubmitCallback.InvokeAsync(monthlyCategoryBudget);
            }
        }
    }
}
