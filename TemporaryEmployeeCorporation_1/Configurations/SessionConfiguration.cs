using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> b)
    {
        b.ToTable("Session");
        b.Property(c => c.SessionName).HasMaxLength(150);
        b.Property(c => c.SessionDescription).HasMaxLength(300);
        b.Property(c => c.Duration)
            .HasComputedColumnSql("DATEDIFF(hour, [DateStart],[DateEnd])")
            .IsRequired();

        b.HasData(new Session
        {
            SessionId = 1,
            CourseId = 1,
            SessionName = "1st Session: Data Types",
            SessionDescription = "Describes the different data types used in programming language c++",
            DateStart = new DateTime(2020, 09, 11,13,0,0),
            DateEnd = new DateTime(2020, 09, 11,15,0,0),
            SessionFee = 500
        });

        b.HasData(new Session
        {
            SessionId = 2,
            CourseId = 1,
            SessionName = "2nd Session: Classes and Objects",
            SessionDescription = "Explicitly defines the difference about classes and objects",
            DateStart = new DateTime(2020, 09, 11, 13, 0, 0),
            DateEnd = new DateTime(2020, 09, 11, 15, 0, 0),
            SessionFee = 500
        });
        b.HasData(new Session
        {
            SessionId = 3,
            CourseId = 2,
            SessionName = "1st Session: Proper Naming Convention",
            SessionDescription = "Proper Naming Convention",
            DateStart = new DateTime(2020, 09, 11, 13, 0, 0),
            DateEnd = new DateTime(2020, 09, 11, 15, 0, 0),
            SessionFee = 500
        });
        b.HasData(new Session
        {
            SessionId = 4,
            CourseId = 3,
            SessionName = "1st Session: CS50",
            SessionDescription = "Topics include abstraction, algorithms and data structures",
            DateStart = new DateTime(2020, 09, 11, 13, 0, 0),
            DateEnd = new DateTime(2020, 09, 11, 15, 0, 0),
            SessionFee = 500
        });
        b.HasData(new Session
        {
            SessionId = 5,
            CourseId = 3,
            SessionName = "1st Session: CS50",
            SessionDescription = "Topics include encapsulation, resource management, security, software engineering, and web development.",
            DateStart = new DateTime(2020, 09, 11, 13, 0, 0),
            DateEnd = new DateTime(2020, 09, 11, 15, 0, 0),
            SessionFee = 500
        });
    }
}