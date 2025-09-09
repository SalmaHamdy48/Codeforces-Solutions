using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversitySystem.Models;

namespace UniversitySystem.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Cname)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Hours)
            .IsRequired();
    }
}