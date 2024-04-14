using System;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Dto;

public class CourseSession
{
    public int SessionId { get; set; }
    public string SessionName { get; set; }
    public string SessionDescription { get; set; }
    public int Duration { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public float SessionFee { get; set; }

    public CourseSession(Session session)
    {
        SessionId = session.SessionId;
        SessionName = session.SessionName;
        SessionDescription = session.SessionDescription;
        SessionFee = session.SessionFee;
        Duration = session.Duration;
        DateStart = session.DateStart;
        DateEnd = session.DateEnd;
    }
}