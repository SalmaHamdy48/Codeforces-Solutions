
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Behaviour;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Validators;
using UniversitySystem.Features.Student.Command.Validators;
using UniversitySystem.Middleware;
using UniversitySystem.Repositories.Implementations;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Features.Course.Command.Handlers;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Add Services --------------------

// 1. Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// 4. AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 5. MediatR// 5. MediatR
builder.Services.AddMediatR(
    typeof(CreateCourseHandler).Assembly,
    Assembly.GetExecutingAssembly());


// 6. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// 7. Scoped Validators (DbContext dependent)
builder.Services.AddScoped<CreateStudentValidator>();
builder.Services.AddScoped<UpdateStudentValidator>();
builder.Services.AddScoped<DeleteStudentValidator>();
builder.Services.AddScoped<CreateCourseValidator>();
builder.Services.AddScoped<UpdateCourseValidator>();
builder.Services.AddScoped<DeleteCourseValidator>();


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// -------------------- Build App --------------------
var app = builder.Build();

// -------------------- Configure Pipeline --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();