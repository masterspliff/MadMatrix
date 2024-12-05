namespace core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted; // Default to NotStarted
        public DateTime StartDate { get; set; } = DateTime.Now; // Default to current date
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1); // Default to 1 day after the start date
        public int? EventId { get; set; } // Optional, can be null
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Default to current date
        public List<int> AssignedToIds { get; set; } = new();
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}

