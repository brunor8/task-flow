namespace TaskFlow.API.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid CreatedById { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TaskItemStatus Status { get; private set; }
    public TaskItemPriority Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }

    protected TaskItem() { }

    public static TaskItem Create(Guid tenantId, Guid projectId, Guid createdById, string title, string? description = null,
                                    TaskItemPriority priority = TaskItemPriority.Medium, DateTime? dueDate = null) =>
        new()
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            ProjectId = projectId,
            CreatedById = createdById,
            Title = title,
            Description = description,
            Status = TaskItemStatus.Todo,
            Priority = priority,
            CreatedAt = DateTime.UtcNow,
            DueDate = dueDate
        };


    // Assignment
    //public void UpdateStatus(TaskItemStatus status) => Status = status;
    public void AssignTo(Guid userId) => AssignedToId = userId;
    public void Unassign() => AssignedToId = null;

    // Status Lifecicle
    public void Start() => Status = TaskItemStatus.InProgress;
    public void Complete() => Status = TaskItemStatus.Done;
    public void Cancel() => Status = TaskItemStatus.Cancelled;
    public void Reopen() => Status = TaskItemStatus.Todo;

    // Data Updates
    public void UpdateTitle(string title) => Title = title;
    public void UpdateDescription(string? description) => Description = description;
    public void UpdatePriority(TaskItemPriority priority) => Priority = priority;
    public void UpdateDueDate(DateTime? dueDate) => DueDate = dueDate;

}


public enum TaskItemStatus
    {
        Todo,
        InProgress,
        Done,
        Cancelled
    }

public enum TaskItemPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
