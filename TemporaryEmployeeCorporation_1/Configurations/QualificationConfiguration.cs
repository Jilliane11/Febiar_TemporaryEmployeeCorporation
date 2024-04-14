using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class QualificationConfiguration : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> b)
    {
        b.ToTable("Qualification");
        b.HasIndex(c => c.Code).IsUnique();
        b.Property(c => c.Description)
            .HasComputedColumnSql("[Type] + ',' + [Specialization]")
            .IsRequired();
        b.Property(c => c.Description).HasMaxLength(250);

        b.HasData(new Qualification { QualificationId = 1, Code = "PRG-C++", Type = "Programmer", Specialization = "C++" });
        b.HasData(new Qualification { QualificationId = 2, Code = "PRG-C#", Type = "Programmer", Specialization = "C#" });
        b.HasData(new Qualification { QualificationId = 3, Code = "SEC-45", Type = "Secretarial Work", Specialization = "at least 45 words per minute" });
        b.HasData(new Qualification { QualificationId = 4, Code = "SEC-60", Type = "Secretarial Work", Specialization = "at least 60 words per minute" });
        b.HasData(new Qualification { QualificationId = 5, Code = "CLERK", Type = "General clerking work", Specialization = "." });
        b.HasData(new Qualification { QualificationId = 6, Code = "PRG-VB", Type = "Programmer", Specialization = "Visual Basic" });
        b.HasData(new Qualification { QualificationId = 7, Code = "DBA-ORA", Type = "Database Administrator", Specialization = "Oracle" });
        b.HasData(new Qualification { QualificationId = 8, Code = "DBA-DB2", Type = "Database Administrator", Specialization = "IDMDB2" });
        b.HasData(new Qualification { QualificationId = 9, Code = "DBA-SQLSERV", Type = "Database Administrator", Specialization = "MS SQL Server" });
        b.HasData(new Qualification { QualificationId = 10, Code = "SYS-1", Type = "Systems Analyst", Specialization = "level 1" });
        b.HasData(new Qualification { QualificationId = 11, Code = "SYS-2", Type = "Systems Analyst", Specialization = "level 2" });
        b.HasData(new Qualification { QualificationId = 12, Code = "NW-NOV", Type = "Network Administrator", Specialization = "Novell experience" });
        b.HasData(new Qualification { QualificationId = 13, Code = "WD-CF", Type = "Web Developer", Specialization = "ColdFusion" });
    }
}