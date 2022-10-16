using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;

namespace Teban.UI.Shared.Accounts
{
    [Authorize]
    public partial class AccountsList
    {
        [Parameter]
        public IEnumerable<Account> Accounts { get; set; }
    }
}
