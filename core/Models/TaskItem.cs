namespace core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted; // Default to NotStarted
        
        public DateTime Date { get; set; } // Represents the task's date

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int? EventId { get; set; } // Optional, can be null
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Default to current date
        public List<int> AssignedToIds { get; set; } = new List<int>();     
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}

