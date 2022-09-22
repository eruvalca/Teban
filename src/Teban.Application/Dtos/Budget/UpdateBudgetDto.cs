using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teban.Application.Dtos.Budget
{
    public class UpdateBudgetDto
    {
        public int BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
