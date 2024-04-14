using System.ComponentModel.DataAnnotations;

namespace TemporaryEmployeeCorporation_1.Core;

public class Opening
{
    public int OpeningId { get; set; }
    [Required]
    public int QualificationId { get; set; }
    public double HourlyPay { get; set; }
    public string OpeningDescription { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }

    //relationships
    public ICollection<Placement>? Placements { get; set; }
    public Qualification QualificationLink { get; set; }
    public int CompanyId { get; set; }
    public Company CompanyLink { get; set; }
}