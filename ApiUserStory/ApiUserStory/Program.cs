using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiUserStory.Data;
using ApiUserStory.Middleware;
using ApiUserStory.Models;
using ApiUserStory.Repositories;
using ApiUserStory.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();



builder.Services.AddScoped<EmailService>();   
builder.Services.AddScoped<IAuthService, AuthService>();

Console.WriteLine("=== TEST OTP CONSOLE ===");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiUserStory v1");
        c.RoutePrefix = string.Empty; 
    });
}


app.UseExceptionHandlingMiddleware();
app.UseStaticFiles(); 


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();