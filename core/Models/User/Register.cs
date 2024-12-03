namespace core.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Data Transfer Object for user registration
/// </summary>
public class RegisterDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public List<Competency> Competencies { get; set; } = new();
}
