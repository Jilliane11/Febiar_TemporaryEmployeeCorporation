using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> b)
    {
        b.ToTable("Candidate");
        b.Property(c => c.CandidateName).HasMaxLength(100);
        b.Property(c => c.HomeAddress).HasMaxLength(250);
        b.Property(c => c.WorkAddress).HasMaxLength(250);
        b.Property(c => c.EmailAddress).HasMaxLength(50);


        b.HasData(new Candidate
        {
            CandidateId = 1, CandidateName = "Cecilia Chapman",
            HomeAddress = "606-3727 Ullamcorper. Street Roseville NH 11523",
            WorkAddress = "Ap #867-859 Sit Rd. Azusa New York 39531", EmailAddress = "ceciliacute@gmail.com",
            TelephoneNumber = "(793) 151-6230"
        });

        b.HasData(new Candidate
        {
            CandidateId = 2,
            CandidateName = "Theodore Lowe",
            HomeAddress = "191-103 Integer Rd. Corona New Mexico 08219",
            WorkAddress = "511-5762 At Rd. Chelsea MI 67708",
            EmailAddress = "theodorecute@gmail.com",
            TelephoneNumber = "(947) 278-5929"
        });

    }
}