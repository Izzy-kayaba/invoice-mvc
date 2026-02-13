// using InvoiceGenerator.Web.Services;
// using InvoiceGenerator.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args); // Instance of webApplication builder combining Startup.cs + Program.cs

// 1. Register services
builder.Services.AddControllers();

// 2. Build the ASP.NET Core app itself
var app = builder.Build();

// 3. Configure middleware
app.UseAuthentication();
app.UseAuthorization();

// 4. Map endpoints
app.MapControllers();

// 5. Run the app
app.Run();