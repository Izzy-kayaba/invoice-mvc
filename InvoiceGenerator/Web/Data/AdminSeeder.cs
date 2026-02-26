using InvoiceGenerator.Data;
using InvoiceGenerator.Models.Entities;

public static class AdminSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!context.Users.Any(u => u.Role == UserRole.Admin))
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "System Admin",
                Email = "admin@invoice.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}