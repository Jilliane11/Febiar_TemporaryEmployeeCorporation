using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Dto
{
    public class CandidateNameDto
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }

        public CandidateNameDto(int candidateId, string candidateName, string homeAddress, string workAddress)
        {
            CandidateId = candidateId;
            CandidateName = candidateName;
            HomeAddress = homeAddress;
            WorkAddress = workAddress;
        }
    }

    public class CandidateDetails
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }

        public string EmailAddress { get; set; }

        public string TelephoneNumber { get; set; }


        public List<CandidateJobHistory> JobHistoryList { get; set; } = new();
        public List<CandidateCertificate> CertificateList { get; set; } = new();
        public List<CandidatePlacement> PlacementList { get; set; } = new();
        public List<CandidateAttendance> AttendanceList { get; set; } = new();

        public CandidateDetails(Candidate candidate)
        {
            if (candidate.Certificates==null)
            {
                throw new InvalidOperationException("A Candidate must possess a qualification of one or more");
            }

            CandidateId = candidate.CandidateId;
            CandidateName = candidate.CandidateName;
            HomeAddress = candidate.HomeAddress;
            WorkAddress = candidate.WorkAddress;
            EmailAddress = candidate.EmailAddress;
            TelephoneNumber = candidate.TelephoneNumber;

            //job history
            var jb= new List<string>();
            JobHistoryList.Clear();
            foreach (var i in candidate.JobHistories)
            {
                if (i.DateAssigned==null) continue;
                
                jb.Add(i.JobHistoryId.ToString());
                jb.Add($"{i.DateAssigned:yyyy MMMM dd}");
                jb.Add($"{i.DateEnded:yyyy MMMM dd}");
                jb.Add(i.JobTitle);
                jb.Add(i.Description);
                jb.Add(i.CompanyName);
                JobHistoryList.Add(new CandidateJobHistory(i));
            }

            //certificate
            var cb=new List<string>();
            CertificateList.Clear();
            foreach (var i in candidate.Certificates)
            {
                if (i.QualificationId==null) continue;
                cb.Add(i.CertificateId.ToString());
                cb.Add(i.Description);
                cb.Add($"{i.DateEarned:yyyy MMMM dd}");

                CertificateList.Add(new CandidateCertificate(i));
            }

            //placements
            var pb = new List<string>();
           PlacementList.Clear();
            foreach (var i in candidate.Placements)
            {
                pb.Add(i.PlacementId.ToString());
                pb.Add($"{i.DateAssigned:yyyy MMMM dd}");
                pb.Add(i.TotalHoursWork.ToString());
                pb.Add(i.PlacementDescription);

                PlacementList.Add(new CandidatePlacement(i));
            }

            //attendance
            var ab = new List<string>();
            AttendanceList.Clear();
            foreach (var i in candidate.Attendances)
            {
                ab.Add(i.AttendanceId.ToString());
                ab.Add(i.SessionDescription);
                ab.Add($"{i.DateAttended:G}");

                AttendanceList.Add(new CandidateAttendance(i));
            }

        }
    }

    public class CandidateJobHistory
    {
        public int JobHistoryId { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime? DateEnded { get; set; }
        public string JobTitle { get; set; }
        public string JobHistoryDescription { get; set; }
        public string CompanyName { get; set; }

        public CandidateJobHistory(JobHistory jobHistory)
        {
            JobHistoryId=jobHistory.JobHistoryId;
            DateAssigned = jobHistory.DateAssigned;
            DateEnded = jobHistory.DateEnded;
            JobTitle = jobHistory.JobTitle;
            JobHistoryDescription = jobHistory.Description;
            CompanyName=jobHistory.CompanyName;
        }
    }

    public class CandidateCertificate
    {
        public int CertificateId {get; set; }
        public string Description { get; set; }
        public DateTime DateEarned { get; set; }
        public int QualificationId { get; set; }

        public CandidateCertificate(Certificate certificate)
        {
            QualificationId=certificate.QualificationId;
            CertificateId=certificate.CertificateId;
            Description = certificate.Description;
            DateEarned=certificate.DateEarned;
        }
    }

    public class CandidatePlacement
    {
        public int PlacementId { get; set; }
        public int TotalHoursWork { get; set; }
        public DateTime DateAssigned { get; set; }
        public string PlacementDescription { get; set; }

        public CandidatePlacement(Placement placement)
        {
            PlacementId=placement.PlacementId;
            PlacementDescription=placement.PlacementDescription;
            TotalHoursWork=placement.TotalHoursWork;
            DateAssigned=placement.DateAssigned;
        }
    }

    public class CandidateAttendance
    {
        public int AttendanceId { get; set; }
        public int SessionId { get; set; }
        public string SessionDescription { get; set; }
        public DateTime DateAttended { get; set; }

        public CandidateAttendance(Attendance attendance)
        {
            SessionId = attendance.SessionId;
            AttendanceId=attendance.AttendanceId;
            SessionDescription = attendance.SessionDescription;
            DateAttended = attendance.DateAttended;
        }

    }

}
