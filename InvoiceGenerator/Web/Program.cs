// using InvoiceGenerator.Web..Services;
// using InvoiceGenerator.Web..Services.Interfaces;
using InvoiceGenerator.Services;
using InvoiceGenerator.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args); // Instance of webApplication builder combining Startup.cs + Program.cs

// Register MVC controllers
builder.Services.AddControllers();


// Register application services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


// 2. Build the ASP.NET Core app itself
var app = builder.Build();

// 3. Configure middleware
app.UseAuthentication();
app.UseAuthorization();

// Enable routing & MVC
app.MapControllers();

// 5. Run the app
app.Run();