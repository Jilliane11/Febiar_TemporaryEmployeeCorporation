namespace TemporaryEmployeeCorporation_1.Core;

public class Course
{
    public int CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }


    //relationships
    //unary relationship
    public ICollection<Course>? Courses { get; set; }
    public int? PrerequisiteCourseId { get; set; }
    public virtual Course? PrerequisiteCourseLink { get; set; }


    //many-to-many relationships
    public ICollection<Session> Sessions { get; set; }
    public ICollection<Requirement> Requirements { get; set; }

}