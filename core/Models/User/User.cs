namespace core.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a user in the system
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier for the user
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// The user's< first name
    /// </summary>
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The user's last name
    /// </summary>
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// The user's email address
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's password
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    
    public List<UserRole> Roles { get; set; } = new();
    public List<Competency> Competencies { get; set; } = new();
    public List<int> EventIds { get; set; } = new();

    // Add list of events to assign user
    // public List<Events> Events ....
    
    public bool EmailNotification { get; set; } = true;
    
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;                                                                                                                                                                                       
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;   
}

public enum UserRole
{
    CoWorker,
    Manager,
    Administrator
}


