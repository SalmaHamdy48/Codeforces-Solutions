using Microsoft.EntityFrameworkCore;
using UniversitySystem.Configurations;
using UniversitySystem.Models;

namespace UniversitySystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
       
    }
}
