using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teban.Domain.Entities;

namespace Teban.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Budget> Budgets { get; }
        public DbSet<Account> Accounts { get; }
        public DbSet<AccountTransaction> AccountTransactions { get; }
        public DbSet<TransactionEntry> TransactionEntries { get; }
        public DbSet<MonthlyCategoryBudget> MonthlyCategoryBudgets { get; }

        Task<int> SaveChangesAsync();

        EntityEntry Entry(object entity);
    }
}
