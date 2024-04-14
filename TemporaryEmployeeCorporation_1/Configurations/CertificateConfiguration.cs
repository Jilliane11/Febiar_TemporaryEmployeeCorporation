using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> b)
    {
        b.ToTable("Certificate");
        b.Property(c => c.DateEarned)
            .HasDefaultValueSql("getdate()");

        b.HasData(new Certificate { CertificateId = 1, QualificationId = 1, CandidateId = 1 ,Description = "Programmer, C++"});
        b.HasData(new Certificate { CertificateId = 2, QualificationId = 1, CandidateId = 2 ,Description = "Programmer, C++"});
        b.HasData(new Certificate { CertificateId = 3, QualificationId = 3, CandidateId = 1 ,Description = "Secretarial Work, at least 45 words per minute"});
    }
}