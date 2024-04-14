using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemporaryEmployeeCorporation_1.Core;

namespace TemporaryEmployeeCorporation_1.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> b)
    {
        b.ToTable("Company");
        b.Property(c => c.CompanyName).HasMaxLength(100);
        b.Property(c => c.Address).HasMaxLength(250);

        b.HasData(new Company
        {
            CompanyId = 1,
            CompanyName = "Stirling Technology Inc.", 
            Address = "711-2880 Nulla St. Mankato Mississippi 96522",
            TelephoneNumber = "(257) 563-7401",
            CompanyEmail = "stirlingcompany@gmail.com"
        });
        b.HasData(new Company
        {
            CompanyId = 2,
            CompanyName = "Miscrosoft Inc.",
            Address = "7342, Maine Street, California",
            TelephoneNumber = "(654) 563-7401",
            CompanyEmail = "miscrosoftcompany@outlook.com"
        });
    }
}