using InvoiceGenerator.Web.Models.Dtos.Auth;

namespace InvoiceGenerator.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    }
}