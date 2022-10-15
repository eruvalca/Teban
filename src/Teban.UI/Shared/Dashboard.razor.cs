using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;
using Teban.UI.Services;

namespace Teban.UI.Shared
{
    [Authorize]
    public partial class Dashboard
    {
        [Inject]
        private BudgetsService BudgetsService { get; set; }

        private List<Budget>? Budgets { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var budgetsRequest = await BudgetsService.GetBudgets();

            if (budgetsRequest.Succeeded)
            {
                Budgets = budgetsRequest.Data?.ToList();
            }
        }
    }
}
