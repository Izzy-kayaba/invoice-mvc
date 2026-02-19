
using System.ComponentModel.DataAnnotations;

namespace InvoiceGenerator.Models.Entities
{
    /// <summary>
    /// Represents application user.
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Stored hashed password.
        /// Never store plain text password.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
