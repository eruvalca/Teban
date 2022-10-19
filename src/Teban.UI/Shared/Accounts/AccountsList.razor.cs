using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;
using Teban.Domain.Enums;

namespace Teban.UI.Shared.Accounts
{
    [Authorize]
    public partial class AccountsList
    {
        [Parameter]
        public IEnumerable<Account> Accounts { get; set; }
        [Parameter]
        public IEnumerable<AccountTransaction> Transactions { get; set; }

        private decimal GetNet()
        {
            return Accounts.Where(a => a.AccountType != AccountType.Category)
                .Sum(a => a.GetAccountBalance(Transactions.Where(t => t.TransactionEntries.Any(te => te.AccountId == a.AccountId))));
        }
    }
}
