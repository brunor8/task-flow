using TaskFlow.Domain.Common;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public UserRole Role { get; set; }
        public Guid? TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<TaskItem>? AssignedTasks { get; set; }
    }
}
