﻿namespace Teban.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int BudgetId { get; set; }
        public int? CategoryGroupId { get; set; }
    }
}
