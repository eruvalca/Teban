using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Entities;

namespace Teban.Application.Dtos.Budget
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Account>? Accounts { get; set; }
        public IEnumerable<CategoryGroup>? CategoryGroups { get; set; }
    }
}
