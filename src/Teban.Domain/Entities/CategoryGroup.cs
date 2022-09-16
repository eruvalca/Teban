namespace Teban.Domain.Entities
{
    public class CategoryGroup
    {
        public int CategoryGroupId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int BudgetId { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
