using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces
{
    public interface ITenantService
    {
        Guid? GetCurrentTenantId();
        string? GetCurrentTenantConnectionString();
        Task<Tenant?> GetCurrentTenantAsync();
        void SetCurrentTenant(Guid tenantId, string connectionString);
    }
}