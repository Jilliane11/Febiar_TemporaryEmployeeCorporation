using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class PlacementConfiguration : IEntityTypeConfiguration<Placement>
{
    public void Configure(EntityTypeBuilder<Placement> b)
    {
        b.ToTable("Placement");

        b.HasMany<JobHistory>(c => c.JobHistories)
            .WithOne(c => c.PlacementLink)
            .HasForeignKey(c => c.PlacementId)
            .OnDelete(DeleteBehavior.NoAction);

        b.HasData(new Placement { PlacementId = 1, CandidateId = 1, TotalHoursWork = 2, OpeningId = 1,DateAssigned = new DateTime(2022,10,06), PlacementDescription = "Stirling Company needs someone who is a Programmer who specializes in C++" });
    }
}