using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO used during login.
/// </summary>
public class LoginRequestDto
{
    [Required]
    public string Email {get; set;} = null!;

    [Required]
    public string Password { get; set; } = null!;
}