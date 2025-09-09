using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Repositories.Implementations;
using FluentValidation;
using MediatR;
using UniversitySystem.Features.Course.Command.Handlers;
using UniversitySystem.Features.Student.Command.Handlers;
using UniversitySystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

// -------------------- DbContext --------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- Repositories --------------------
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// -------------------- AutoMapper --------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// -------------------- FluentValidation --------------------
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// -------------------- MediatR --------------------
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

// -------------------- Handlers (for Controllers without MediatR) --------------------
builder.Services.AddScoped<CreateCourseHandler>();
builder.Services.AddScoped<UpdateCourseHandler>();
builder.Services.AddScoped<DeleteCourseHandler>();

builder.Services.AddScoped<CreateStudentHandler>();
builder.Services.AddScoped<UpdateStudentHandler>();
builder.Services.AddScoped<DeleteStudentHandler>();

// -------------------- Controllers --------------------
builder.Services.AddControllers();

// -------------------- Middleware --------------------
builder.Services.AddCors(); // إذا احتجتي
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------- Middleware pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
