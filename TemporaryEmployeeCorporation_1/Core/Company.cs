namespace TemporaryEmployeeCorporation_1.Core;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string TelephoneNumber { get; set; }
    public string CompanyEmail { get; set; }

    //relationships
    public ICollection<Opening>? Openings { get; set; }
}