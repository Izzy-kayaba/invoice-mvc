using InvoiceGenerator.Data;
using InvoiceGenerator.Repositories;
using InvoiceGenerator.Repositories.Interfaces;
using InvoiceGenerator.Services;
using InvoiceGenerator.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // Instance of webApplication builder combining Startup.cs + Program.cs

// Add EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Register MVC controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// 2. Build the ASP.NET Core app itself
var app = builder.Build();

// 3. Configure middleware
app.UseAuthentication();
app.UseAuthorization();

// Add missing using directive for Swagger middleware

// Enable routing & MVC
app.MapControllers();

// 5. Run the app
app.Run();