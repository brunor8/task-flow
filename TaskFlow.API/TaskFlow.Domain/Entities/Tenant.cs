using TaskFlow.Domain.Common;

namespace TaskFlow.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty ;
        public string ConnectionString { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? SubscriptionExpiry { get; set; }
        public string? DatabaseProvider { get; set; } = "SqlServer";
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
    }
}
