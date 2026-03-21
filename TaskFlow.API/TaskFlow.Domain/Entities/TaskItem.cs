using TaskFlow.Domain.Common;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Domain.Entities
{
    public class TaskItem: BaseEntity, ITenantEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Enums.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public Guid? AssignedToId { get; set; }
        public virtual User? AssignedTo { get; set; }
        public Guid TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
    }
}
