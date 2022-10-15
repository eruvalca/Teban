using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Application.Dtos.Identity;
using Teban.Domain.Entities;
using Teban.UI.Services;

namespace Teban.UI.Pages.Budgets
{
    public partial class BudgetDetail
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private BudgetsService BudgetsService { get; set; }

        [Parameter]
        public int BudgetId { get; set; }

        private TebanUserDto User { get; set; }
        private Budget Budget { get; set; }
        private List<string> ErrorMessages { get; set; } = new List<string>();
        private bool ShowErrors { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            User = await IdentityService.GetUserDetails();

            var budgetRequest = await BudgetsService.GetBudget(BudgetId);

            if (budgetRequest.Succeeded)
            {
                if (budgetRequest.Data is not null)
                {
                    Budget = budgetRequest.Data;
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error retrieving the budget details." };
                }

                ShowErrors = true;
            }
            else
            {
                if (budgetRequest.Errors is not null)
                {
                    ErrorMessages = budgetRequest.Errors.ToList();
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error retrieving the budget details." };
                }

                ShowErrors = true;
            }
        }
    }
}
