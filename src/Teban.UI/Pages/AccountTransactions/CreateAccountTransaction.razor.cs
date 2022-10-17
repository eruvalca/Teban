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

namespace Teban.UI.Pages.AccountTransactions
{
    [Authorize]
    public partial class CreateAccountTransaction
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private IdentityClientService IdentityService { get; set; }
        [Inject]
        private AccountTransactionsService AccountTransactionsService { get; set; }
        [Inject]
        private TransactionEntriesService TransactionEntriesService { get; set; }

        [Parameter]
        public int BudgetId { get; set; }

        private TebanUserDto? User { get; set; }
        private AccountTransaction AccountTransaction { get; set; }
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

            var result = await AccountTransactionsService.PostAccountTransaction(AccountTransaction);

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
                    ErrorMessages = new List<string> { "There was an error creating the transaction." };
                }

                ShowErrors = true;
                DisableSubmit = false;
            }
        }
    }
}
