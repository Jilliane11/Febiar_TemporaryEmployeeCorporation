using System;
using System.Collections.Generic;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Dto;

public class CourseDetails
{
    public int CourseId { get; set; }
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }

    public string PreRequisiteCourseName { get; set; }
    public int PreRequisiteId { get; set; }
    

    public List<CourseSession> SessionList { get; set; } = new();

    public List<CourseRequirement> RequirementList { get; set; } = new();

    public CourseDetails(Course course)
    {
        if (course.Requirements is null)
            throw new InvalidOperationException("Its should have an affiliated qualification");
        if (course.Sessions is null)
            throw new InvalidOperationException("A course should have at least one session");


        CourseId = course.CourseId;
        CourseCode=course.CourseCode;
        CourseName = course.CourseName;
        CourseDescription = course.CourseDescription;

        //PreRequisiteCourseName = course.PrerequisiteCourseLink.CourseName;
        //PreRequisiteId = course.CourseId;
        
        
        //for sessions
        var sb = new List<string>();
        SessionList.Clear();
        foreach (var i in course.Sessions)
        {
            if (i.CourseId == null) return;

            sb.Add(i.SessionId.ToString());
            sb.Add(i.SessionName);
            sb.Add(i.SessionDescription);
            sb.Add(i.Duration.ToString());
            sb.Add(i.SessionFee.ToString());
            sb.Add($"{i.DateStart:yyyy MMMM dd}");
            sb.Add($"{i.DateEnd:yyyy MMMM dd}");

            SessionList.Add(new CourseSession(i));

        }

        //for requirements
        var rb = new List<string>();
        RequirementList.Clear();
        foreach (var i in course.Requirements)
        {
            if (i.RequirementDescription is null) continue;

            rb.Add(i.QualificationId.ToString());
            rb.Add(i.CourseId.ToString());
            rb.Add(i.RequirementId.ToString());
            rb.Add(i.RequirementDescription);


            RequirementList.Add(new CourseRequirement(i));
        }
    }
}