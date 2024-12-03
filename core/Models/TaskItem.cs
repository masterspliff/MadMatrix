namespace core.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public int EventId { get; set; }
    public int AssignedToId { get; set; }
}

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    Blocked
}
