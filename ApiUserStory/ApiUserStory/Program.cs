using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiUserStory.Data;
using ApiUserStory.Middleware;
using ApiUserStory.Models;
using ApiUserStory.Repositories;
using ApiUserStory.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ✅ Register Repositories
// ✅ Register Repositories
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// ✅ Register Services
builder.Services.AddScoped<IAuthService, AuthService>();

Console.WriteLine("=== TEST OTP CONSOLE ===");

// ✅ Add Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiUserStory v1");
        c.RoutePrefix = string.Empty; // Swagger on root
    });
}

// ✅ Middleware
app.UseExceptionHandlingMiddleware();
app.UseStaticFiles(); 

// ✅ Security
app.UseAuthentication();
app.UseAuthorization();

// ✅ Map Controllers
app.MapControllers();

app.Run();