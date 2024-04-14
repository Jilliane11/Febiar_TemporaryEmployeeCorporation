namespace TemporaryEmployeeCorporation_1.Core;

public class Qualification
{
    public int QualificationId { get; set; }
    public string Code { get; set; }
    public string Type { get; set; }
    public string? Specialization { get; set; }
    public string Description { get; set; }

    //relationships
    public ICollection<Certificate> Certificates { get; set; }
    public ICollection<Opening> Openings { get; set; }
    public ICollection<Requirement>? Enrollments { get; set; }

}