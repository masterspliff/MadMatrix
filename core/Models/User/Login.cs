namespace core.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Data Transfer Object for user login
/// </summary>
public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
