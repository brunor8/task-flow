using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Domain.Entities;
using TaskFlow.API.Infrastructure.Multitenancy;

namespace TaskFlow.API.Infrastructure.Persistence;

public class TaskFlowDbContext : DbContext
{
    private readonly TenantContext _tenantContext;
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options, TenantContext tenantContext) : base(options) 
    {
        _tenantContext = tenantContext;
    }
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(p => p.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskItem>()
            .HasOne<Tenant>()
            .WithMany()
            .HasForeignKey(t => t.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskItem>()
            .HasOne<Project>()
            .WithMany()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        modelBuilder.Entity<Project>()
            .HasQueryFilter(p => p.TenantId == _tenantContext.TenantId);

        modelBuilder.Entity<TaskItem>()
            .HasQueryFilter(t => t.TenantId == _tenantContext.TenantId);

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.Priority)
            .HasConversion<string>();
    }
}
