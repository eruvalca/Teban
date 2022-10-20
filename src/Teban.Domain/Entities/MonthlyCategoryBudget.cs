using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class MonthlyCategoryBudget : AuditableEntity
    {
        public int MonthlyCategoryBudgetId { get; set; }
        public DateTime MonthYear { get; set; }
        public decimal Amount { get; set; }

        public int AccountId { get; set; }
    }
}
