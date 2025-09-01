using CompanyApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CompanyApi.Data.Configurations;


public class ManagesConfiguration : IEntityTypeConfiguration<Manages>
{
    public void Configure(EntityTypeBuilder<Manages> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.DepartmentId });


        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Manages)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(x => x.Department)
            .WithMany(d => d.Manages)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}