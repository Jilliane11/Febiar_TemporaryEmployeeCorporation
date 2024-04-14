using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Dto;

public class CourseRequirement
{
    public string RequirementDescription { get; set; }
    public int QualificationId { get; set; }
    public int CourseId { get; set; }

    public CourseRequirement(Requirement requirement)
    {
        CourseId=requirement.CourseId;
        RequirementDescription=requirement.RequirementDescription;
        QualificationId=requirement.QualificationId;
    }
}