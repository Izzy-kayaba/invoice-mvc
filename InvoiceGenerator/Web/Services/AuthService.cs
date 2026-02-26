using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Repositories.Interfaces;
using InvoiceGenerator.Services.Interfaces;
using InvoiceGenerator.Web.Models.Dtos.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceGenerator.Services
{
    /// <summary>
    /// Handles authentication business logic.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email.Trim().ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Invalid Credentials");

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(jwtSettings["ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
            
            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Id = created.Id,
                Email = created.Email,
                Role = created.Role.ToString(),
                Token = token
            };
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
