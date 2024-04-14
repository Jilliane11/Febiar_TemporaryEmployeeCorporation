using System.ComponentModel.DataAnnotations;

namespace TemporaryEmployeeCorporation_1.Core;

public class Session
{
    public int SessionId { get; set; }
    public string SessionName { get; set; }
    public string SessionDescription { get; set; }
    public int Duration { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public float SessionFee { get; set; }

    //relationships

    public int CourseId { get; set; }
    public Course CourseLink { get; set; }
    public ICollection<Attendance> Attendances { get; set; }
}