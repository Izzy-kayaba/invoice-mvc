

using InvoiceGenerator.Data;
using InvoiceGenerator.Models.Entities;
using InvoiceGenerator.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Repositories
{

    /// <summary>
    /// Handles DB operations for users.
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}