using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Repositories.Interfaces;
using InvoiceGenerator.Services.Interfaces;
using InvoiceGenerator.Web.Models.Dtos.Auth;

namespace InvoiceGenerator.Services
{
    /// <summary>
    /// Handles authentication business logic.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email.Trim().ToLower(), // Normalization for email uniqueness
                PasswordHash = HashPassword(dto.Password),
                Role = UserRole.Viewer,  // Secure default role
                CreatedAt = DateTime.UtcNow
            };

            var created = await _userRepository.CreateAsync(user);

            return new AuthResponseDto
            {
                Id = created.Id,
                Email = created.Email,
                Role = created.Role.ToString()
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
