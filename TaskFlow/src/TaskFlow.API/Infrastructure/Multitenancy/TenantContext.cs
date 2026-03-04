using TaskFlow.API.Domain.Entities;

namespace TaskFlow.API.Infrastructure.Multitenancy;

public class TenantContext
{
    public Guid TenantId { get; set; }
}
