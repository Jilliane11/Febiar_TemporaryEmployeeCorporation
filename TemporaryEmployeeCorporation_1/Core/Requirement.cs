namespace TemporaryEmployeeCorporation_1.Core;

public class Requirement
{
    public int RequirementId { get; set; }
    public string RequirementDescription { get; set; }

    //relationships
    public int QualificationId { get; set; }
    public Qualification QualificationLink { get; set; }

    public int CourseId { get; set; }
    public Course CourseLink { get; set; }
}