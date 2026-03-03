namespace TaskFlow.API.Domain.Entities;

public class Tenant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<User> _users = new();
    private readonly List<Project> _projects = new();

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

    protected Tenant() { }

    public static Tenant Create(string name, string slug) =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Slug = slug.ToLower().Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void UpdateName(string name) => Name = name;
}
