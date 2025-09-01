using CompanyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CompanyApi.Data.Configurations;


public class WorksOnHoursConfiguration : IEntityTypeConfiguration<WorksOnHours>
{
    public void Configure(EntityTypeBuilder<WorksOnHours> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.ProjectId });
        builder.Property(x => x.Hours).IsRequired();


        builder.HasOne(x => x.Employee)
            .WithMany(e => e.WorksOn)
            .HasForeignKey(x => x.EmployeeId);


        builder.HasOne(x => x.Project)
            .WithMany(p => p.WorksOn)
            .HasForeignKey(x => x.ProjectId);
    }
}