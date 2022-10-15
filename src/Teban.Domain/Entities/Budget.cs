using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class Budget : AuditableEntity
    {
        public int BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string TebanUserId { get; set; }
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<CategoryGroup>? CategoryGroups { get; set; }
    }
}
