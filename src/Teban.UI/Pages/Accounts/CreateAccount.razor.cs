using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Application.Dtos.Identity;
using Teban.Domain.Entities;
using Teban.Domain.Enums;
using Teban.UI.Services;

namespace Teban.UI.Pages.Accounts
{
    [Authorize]
    public partial class CreateAccount
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private AccountsService AccountsService { get; set; }

        [Parameter]
        public int BudgetId { get; set; }

        private TebanUserDto? User { get; set; }
        private Account Account { get; set; } = new Account();
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

            Account.BudgetId = BudgetId;
            Account.CreatedBy = User.UserId;

            if (Account.AccountType == AccountType.Credit)
            {
                Account.StartingBalance *= -1;
            }

            var result = await AccountsService.PostAccount(Account);

            if (result.Succeeded)
            {
                Navigation.NavigateTo($"/budget/{BudgetId}");
            }
            else
            {
                if (result.Errors is not null)
                {
                    ErrorMessages = result.Errors.ToList();
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error creating the account." };
                }

                ShowErrors = true;
                DisableSubmit = false;
            }
        }
    }
}
