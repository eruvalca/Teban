using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Teban.Application.Interfaces;
using Teban.Domain.Entities;
using Teban.Infrastructure.Identity;
using Teban.Infrastructure.Persistence.Configuration;

namespace Teban.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<TebanUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AccountTransaction> AccountTransactions => Set<AccountTransaction>();
        public DbSet<TransactionEntry> TransactionEntries => Set<TransactionEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Budget>()
                .HasOne<TebanUser>()
                .WithMany(t => t.Budgets);

            modelBuilder.Entity<Account>()
                .HasOne<TebanUser>()
                .WithMany(t => t.Accounts);

            modelBuilder.Entity<Account>(a =>
            {
                a.Property(a => a.StartingBalance).HasColumnType("money");
            });

            modelBuilder.Entity<TransactionEntry>(t =>
            {
                t.Property(t => t.Amount).HasColumnType("money");
            });

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }
    }
}
