using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teban.Application.Models;
using Teban.Application.Persistence.Configuration;
using Teban.Application.Persistence.Interceptors;

namespace Teban.Application.Persistence;
public class ApplicationDbContext : IdentityDbContext<TebanUser>
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
    : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CommunicationSchedule> CommunicationSchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}