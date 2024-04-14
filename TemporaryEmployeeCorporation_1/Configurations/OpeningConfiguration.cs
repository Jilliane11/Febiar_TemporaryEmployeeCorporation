using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class OpeningConfiguration : IEntityTypeConfiguration<Opening>
{
    public void Configure(EntityTypeBuilder<Opening> b)
    {
        b.ToTable("Opening");

        b.HasData(new Opening { OpeningId = 1, CompanyId = 1, DateStart = new DateTime(2021, 10, 10), HourlyPay = 30.45, QualificationId = 1 ,DateEnd = new DateTime(2022,09,4),OpeningDescription = "Stirling Company needs someone who is a Programmer who specializes in C++"});
        b.HasData(new Opening { OpeningId = 2, CompanyId = 2, DateStart = new DateTime(2021, 10, 10), HourlyPay = 40.45, QualificationId = 2 ,DateEnd = new DateTime(2022,09,4),OpeningDescription = "Microsoft needs a Programmer who specializes in C#"});
    }
}