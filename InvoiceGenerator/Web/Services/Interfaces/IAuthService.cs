using InvoiceGenerator.Web.Models.Dtos.Auth;

namespace InvoiceGenerator.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    }
}