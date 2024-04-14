namespace TEC_App.Dto;

public class CourseNameDto
{
    public int CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }

    public CourseNameDto(int courseId, string courseCode, string courseName, string courseDescription)
    {
        CourseId = courseId;
        CourseCode = courseCode;
        CourseName = courseName;
        CourseDescription = courseDescription;
    }
}