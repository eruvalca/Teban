using Teban.Domain.Base;

namespace Teban.Domain.Entities
{
    public class Budget : AuditableEntity
    {
        public int BudgetId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public ICollection<Account>? Accounts { get; set; }
    }
}
