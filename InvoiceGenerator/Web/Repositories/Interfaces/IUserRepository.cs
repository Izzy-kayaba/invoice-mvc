

using InvoiceGenerator.Models.Entities;

namespace InvoiceGenerator.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}