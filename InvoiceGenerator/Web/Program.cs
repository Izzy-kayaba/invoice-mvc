using InvoiceGenerator.Data;
using InvoiceGenerator.Repositories;
using InvoiceGenerator.Repositories.Interfaces;
using InvoiceGenerator.Services;
using InvoiceGenerator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args); // Instance of webApplication builder combining Startup.cs + Program.cs

// Add EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArtecoConnection")));

// Dependency Injection
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Settings
var jwtSettings = builder.Configuration.GetSection("Jwt");

// ✅ REGISTER AUTHENTICATION BEFORE Build()
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});


// Register MVC controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await AdminSeeder.SeedAsync(db);
}
// 3. Configure middleware
app.UseAuthentication();
app.UseAuthorization();

// Add missing using directive for Swagger middleware

// Enable routing & MVC
app.MapControllers();

// 5. Run the app
app.Run();