using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> b)
    {
        b.ToTable("Course");
        b.HasIndex(c => c.CourseCode).IsUnique();
        b.Property(c => c.CourseName).HasMaxLength(150);
        b.Property(c => c.CourseDescription).HasMaxLength(250);

        b.HasMany(c => c.Courses)
            .WithOne(c => c.PrerequisiteCourseLink)
            .HasForeignKey(c => c.PrerequisiteCourseId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientNoAction);


        b.HasData(new Course {CourseCode = "PROG01-21", PrerequisiteCourseId = 2, CourseId = 1, CourseName = "Introduction to Programming C++", CourseDescription = "Pre-requisite needed" });
        b.HasData(new Course {CourseCode = "PROG02-21", CourseId = 2, CourseName = "Basics to Programming C++", CourseDescription = "Tackles about the basics of C++ language" });
        b.HasData(new Course {CourseCode = "CS50G01-21", CourseId = 3, CourseName = "CS50's Introduction to Computer Science", CourseDescription = "Teaches students how to think algorithmically and solve problems efficiently" });
        b.HasData(new Course {CourseCode = "AI01-21", CourseId = 4, CourseName = "Elements of AI", CourseDescription = "Beginner level of AI" });
    }
}