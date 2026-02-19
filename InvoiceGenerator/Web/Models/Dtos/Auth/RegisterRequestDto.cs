using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Web.Models.Dtos.Auth;

public class RegisterRequestDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public string Role { get; set; } = "Viewer"; // Default role
}

