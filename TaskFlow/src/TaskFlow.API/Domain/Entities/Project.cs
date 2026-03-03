namespace TaskFlow.API.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt {  get; private set; }
    public bool IsArchived { get; private set; } = false;

    private readonly List<TaskItem> _tasks = new();
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();

    protected Project() { }

    public static Project Create(Guid tenantId, Guid ownerId, string name, string? description = null) =>
        new()
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            OwnerId = ownerId,
            Name = name,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };

    public void UpdateName(string name) => Name = name;

    public void UpdateDescription(string? description) => Description = description;

    public void Archive() => IsArchived = true;

    public void Restore() => IsArchived = false;

    public void TransferOwnership(Guid newOwnerId) => OwnerId = newOwnerId;
}
