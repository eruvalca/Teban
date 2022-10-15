using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public partial class CreateBudget
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private BudgetsService BudgetsService { get; set; }

        private TebanUserDto? User { get; set; }
        private Budget Budget { get; set; } = new Budget();
        private List<string> ErrorMessages { get; set; } = new List<string>();
        private bool ShowErrors { get; set; } = false;
        private bool DisableSubmit { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            User = await IdentityService.GetUserDetails();
        }

        private async Task HandleSubmit()
        {
            DisableSubmit = true;

            Budget.TebanUserId = User.UserId;
            Budget.Created = DateTime.UtcNow;
            Budget.CreatedBy = User.UserId;

            var result = await BudgetsService.PostBudget(Budget);

            if (result.Succeeded)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                if (result.Errors is not null)
                {
                    ErrorMessages = result.Errors.ToList();
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error creating the budget." };
                }

                ShowErrors = true;
                DisableSubmit = false;
            }
        }
    }
}
