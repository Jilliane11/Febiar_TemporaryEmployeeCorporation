using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class JobHistoryConfiguration : IEntityTypeConfiguration<JobHistory>
{
    public void Configure(EntityTypeBuilder<JobHistory> b)
    {
        b.ToTable("JobHistory");
        b.HasOne<Placement>(c => c.PlacementLink)
            .WithMany(c => c.JobHistories)
            .HasForeignKey(c => c.PlacementId)
            .OnDelete(DeleteBehavior.NoAction);


        b.HasData(new JobHistory {JobHistoryId = 1, DateAssigned = new DateTime(2021,03,02), CandidateId = 1, CompanyName = "Microsoft Inc.", JobTitle = "Junior Web Developer", PlacementId = 1 ,Description = "Web Developer",DateEnded = new DateTime(2022,01,03)});
    }
}