using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TEC_App.Dto;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class EditCandidateViewModel:AddCandidateViewModel
    {
        private readonly CandidateDetails _candidate;
        public EditCandidateViewModel(TECContext context,CandidateDetails candidate) : base(context)
        {
            _candidate=candidate;
            CandidateName = candidate.CandidateName;
            HomeAddress=candidate.HomeAddress;
            WorkAddress=candidate.WorkAddress;
            PhoneNumber = candidate.TelephoneNumber;
            EmailAddress=candidate.EmailAddress;

            SearchQualification = " ";
            SearchSession = " ";

            //certificates
            foreach (var certificate in candidate.CertificateList)
            {
                CandidateCertificates.Add(certificate);
            }

            //sessions attended
            foreach (var attendance in candidate.AttendanceList)
            {
                CandidateAttendances.Add(attendance);
            }

            //job history
            foreach (var jobHistory in candidate.JobHistoryList)
            {
                CandidateJobHistories.Add(jobHistory);
            }


        }

        public override void SaveCandidate()
        {
            var cand = _context.Candidates.First(c => c.CandidateId == _candidate.CandidateId);

            //update

            cand.CandidateName = CandidateName;
            cand.HomeAddress = HomeAddress;
            cand.WorkAddress = WorkAddress;
            cand.TelephoneNumber = PhoneNumber;
            cand.EmailAddress = EmailAddress;


            //update cert
            cand.Certificates.Clear();
            foreach (var cert in CandidateCertificates)
            {
                cand.Certificates.Add(new Certificate
                {
                    CandidateId = cand.CandidateId,
                    QualificationId = cert.QualificationId,
                    Description = cert.Description,
                    DateEarned = cert.DateEarned
                });
            }
            //for sessions attended
            cand.Attendances.Clear();
            foreach (var i in CandidateAttendances)
            {
                cand.Attendances.Add(new Attendance
                {
                    SessionId = i.SessionId,
                    CandidateId = cand.CandidateId,
                    DateAttended = i.DateAttended,
                    SessionDescription = i.SessionDescription
                });
            }

            //for job history
            cand.JobHistories.Clear();

            foreach (var i in CandidateJobHistories)
            {
                cand.JobHistories.Add(new JobHistory
                {
                    CandidateId = cand.CandidateId,
                    DateAssigned = i.DateAssigned,
                    DateEnded = i.DateEnded,
                    JobTitle = i.JobTitle,
                    Description = i.JobHistoryDescription,
                    CompanyName = i.CompanyName,

                });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }



        }
    }
}
