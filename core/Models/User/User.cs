namespace core.Models;

public class User // user in system
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    
    public List<UserRole> Roles { get; set; } = new();
    public List<Competencies> Competencies { get; set; } = new();

    // Add list of events to assign user
    // public List<Events> Events ....
    
    public bool EmailNotification { get; set; } = true;
    
    public DateTime LastLogin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

