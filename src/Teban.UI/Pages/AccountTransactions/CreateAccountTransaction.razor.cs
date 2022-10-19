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
        private AccountsService AccountsService { get; set; }
        [Inject]
        private AccountTransactionsService AccountTransactionsService { get; set; }
        [Inject]
        private TransactionEntriesService TransactionEntriesService { get; set; }

        [Parameter]
        public int BudgetId { get; set; }

        private TebanUserDto? User { get; set; }
        private AccountTransaction AccountTransaction { get; set; } = new AccountTransaction();
        private List<Account> Accounts { get; set; }
        private Account NewCategoryAccount { get; set; } = new();
        private int CategoryId { get; set; }
        private decimal Amount { get; set; }
        private int AccountFromId { get; set; }
        private int AccountToId { get; set; }
        private List<string> ErrorMessages { get; set; } = new List<string>();
        private bool ShowErrors { get; set; } = false;
        private bool DisableSubmit { get; set; } = false;
        private bool ShowNewCategoryInput { get; set; } = false;
        private bool DisableCategorySubmit { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            User = await IdentityService.GetUserDetails();
            await GetAccounts();
        }

        private async Task HandleSubmit()
        {
            DisableSubmit = true;

            AccountTransaction.BudgetId = BudgetId;
            AccountTransaction.TransactionDate = AccountTransaction.TransactionDate.ToUniversalTime();
            AccountTransaction.CreatedBy = User.UserId;

            var result = await AccountTransactionsService.PostAccountTransaction(AccountTransaction);

            if (result.Succeeded)
            {
                AccountTransaction = result.Data;

                List<TransactionEntry> entries = new();

                if (AccountTransaction.IsInflow)
                {
                    var accountToEntry = new TransactionEntry
                    {
                        Amount = Amount,
                        AccountId = AccountToId,
                        CreatedBy = User.UserId,
                        AccountTransactionId = AccountTransaction.AccountTransactionId
                    };

                    entries.Add(accountToEntry);

                    var incomeEntry = new TransactionEntry
                    {
                        Amount = Amount * -1,
                        AccountId = CategoryId,
                        CreatedBy = User.UserId,
                        AccountTransactionId = AccountTransaction.AccountTransactionId
                    };

                    entries.Add(incomeEntry);
                }
                else
                {
                    if (AccountTransaction.IsTransfer)
                    {
                        var accountFromEntry = new TransactionEntry
                        {
                            Amount = Amount * -1,
                            AccountId = AccountFromId,
                            CreatedBy = User.UserId,
                            AccountTransactionId = AccountTransaction.AccountTransactionId
                        };

                        entries.Add(accountFromEntry);

                        var accountToEntry = new TransactionEntry
                        {
                            Amount = Amount,
                            AccountId = AccountToId,
                            CreatedBy = User.UserId,
                            AccountTransactionId = AccountTransaction.AccountTransactionId
                        };

                        entries.Add(accountToEntry);
                    }
                    else
                    {
                        var accountFromEntry = new TransactionEntry
                        {
                            Amount = Amount * -1,
                            AccountId = AccountFromId,
                            CreatedBy = User.UserId,
                            AccountTransactionId = AccountTransaction.AccountTransactionId
                        };

                        entries.Add(accountFromEntry);

                        var expenseEntry = new TransactionEntry
                        {
                            Amount = Amount,
                            AccountId = CategoryId,
                            CreatedBy = User.UserId,
                            AccountTransactionId = AccountTransaction.AccountTransactionId
                        };

                        entries.Add(expenseEntry);
                    }
                }

                var entriesResult = await TransactionEntriesService.PostTransactionEntryBatch(entries);

                if (entriesResult.Succeeded)
                {
                    if (entriesResult.Data > 0)
                    {
                        Navigation.NavigateTo($"/budget/{BudgetId}");
                    }
                    else
                    {
                        if (entriesResult.Errors is not null)
                        {
                            ErrorMessages = entriesResult.Errors.ToList();
                        }
                        else
                        {
                            ErrorMessages = new List<string> { "There was an error creating the transaction. Please refresh or try again later." };
                        }

                        ShowErrors = true;
                        DisableSubmit = false;
                    }
                }
                else
                {
                    if (entriesResult.Errors is not null)
                    {
                        ErrorMessages = entriesResult.Errors.ToList();
                    }
                    else
                    {
                        ErrorMessages = new List<string> { "There was an error creating the transaction. Please refresh or try again later." };
                    }

                    ShowErrors = true;
                    DisableSubmit = false;
                }
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

        private async Task HandleCategorySubmit()
        {
            DisableCategorySubmit = true;

            NewCategoryAccount.AccountType = AccountType.Category;
            NewCategoryAccount.BudgetId = BudgetId;
            NewCategoryAccount.CreatedBy = User.UserId;

            var result = await AccountsService.PostAccount(NewCategoryAccount);

            if (result.Succeeded)
            {
                await GetAccounts();
                NewCategoryAccount = new Account();
                ShowNewCategoryInput = false;
                DisableCategorySubmit = false;
            }
            else
            {
                if (result.Errors is not null)
                {
                    ErrorMessages = result.Errors.ToList();
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error creating the category." };
                }

                ShowErrors = true;
                ShowNewCategoryInput = false;
                DisableCategorySubmit = false;
            }
        }

        private async Task GetAccounts()
        {
            var accountsRequest = await AccountsService.GetAccountsByBudget(BudgetId);

            if (accountsRequest.Succeeded)
            {
                if (accountsRequest.Data is not null && accountsRequest.Data.Any())
                {
                    Accounts = accountsRequest.Data.ToList();
                }
                else
                {
                    if (accountsRequest.Errors is not null)
                    {
                        ErrorMessages = accountsRequest.Errors.ToList();
                    }
                    else
                    {
                        ErrorMessages = new List<string> { "There was an error retrieving user accounts. Please refresh or try again later." };
                    }

                    ShowErrors = true;
                    DisableSubmit = false;
                }
            }
            else
            {
                if (accountsRequest.Errors is not null)
                {
                    ErrorMessages = accountsRequest.Errors.ToList();
                }
                else
                {
                    ErrorMessages = new List<string> { "There was an error retrieving user accounts. Please refresh or try again later." };
                }

                ShowErrors = true;
                DisableSubmit = false;
            }
        }
    }
}
