using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class RequirementConfiguration : IEntityTypeConfiguration<Requirement>
{
    public void Configure(EntityTypeBuilder<Requirement> b)
    {
        b.ToTable("Requirement");

        b.HasData(new Requirement { RequirementId = 1, QualificationId = 1, CourseId = 1 , RequirementDescription = "Course for Programming Qualification"});
    }
}