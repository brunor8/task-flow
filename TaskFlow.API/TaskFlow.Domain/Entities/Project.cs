using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Common;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Domain.Entities
{
    public class Project : BaseEntity, ITenantEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid OwnerId { get; set; }
        public virtual User? Owner { get; set; }
        public Guid TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<TaskItem>? Tasks { get; set; }
    }
}
