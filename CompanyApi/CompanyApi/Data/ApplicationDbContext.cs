using CompanyApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CompanyApi.Data;


public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Dependent> Dependents => Set<Dependent>();
    public DbSet<WorksOnHours> WorksOnHours => Set<WorksOnHours>();
    public DbSet<Manages> Manages => Set<Manages>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}