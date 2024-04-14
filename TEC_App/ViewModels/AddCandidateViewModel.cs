using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TEC_App.Dto;
using TEC_App.Parts;
using TEC_App.Parts.QualificationParts;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddCandidateViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion

        #region Fields

        protected TECContext _context;
        private string _searchQualification;
        private string _searchSession;
        

        #endregion

        public AddCandidateViewModel(TECContext context)
        {
            _context = context;

            SearchQualification = string.Empty;
            SearchSession=string.Empty;
            FilterSessions();
            FilterQualifications();

        }
        
        private void FilterQualifications()
        {
            Qualifications.Clear();

            var quals = _context.Qualifications
                .Where(c => c.Description.ToLower().Contains(SearchQualification))
                .Take(10)
                .ToList();

            foreach (var i in quals)
            {
                Qualifications.Add(i);
            }
        }
        private void FilterSessions()
        {
            Sessions.Clear();

            var sessions = _context.Sessions
                .Where(c => c.SessionName.ToLower().Contains(SearchSession))
                .Take(10)
                .ToList();

            foreach (var i in sessions)
            {
                Sessions.Add(i);   
            }
        }


        
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }
        public string PhoneNumber { get; set; }     
        public string EmailAddress { get; set; }
        public Qualification SelectedQualification { get; set; }
        public Session SelectedSession { get; set; }
        public ObservableCollection<Qualification> Qualifications { get; set; } = new();
        public ObservableCollection<Session> Sessions { get; set; }=new();
        public ObservableCollection<JobHistory> JobHistories { get; set; }=new();
        public string SearchQualification
        {
            get => _searchQualification;
            set
            {
                _searchQualification = value;
                FilterQualifications();

                SelectedQualification = Qualifications.Count > 0 ? Qualifications[0] : null;

                OnPropertyChanged(nameof(SelectedQualification));
            }
        }

        public string SearchSession
        {
            get=> _searchSession;
            set
            {
                _searchSession=value;
                FilterSessions();

                SelectedSession = Sessions.Count > 0 ? Sessions[0] : null;

                OnPropertyChanged(nameof(SelectedSession));
            }
        }
        
        public ObservableCollection<CandidateCertificate> CandidateCertificates { get; set; } = new();
        public ObservableCollection<CandidateAttendance> CandidateAttendances { get; set; } = new();
        public ObservableCollection<CandidateJobHistory> CandidateJobHistories { get; set; }=new();

        //certificate
        public string Description { get; set; }
        public DateTime DateEarned { get; set; }
        public void AddCertificate()
        {
            var cert = new Certificate();
            cert.CandidateId = CandidateId;
            cert.QualificationId = SelectedQualification.QualificationId;
            cert.Description = Description;
            cert.DateEarned = DateEarned;

            CandidateCertificates.Add(new CandidateCertificate(cert));
        }
        public string SessionDescription { get; set; }
        public DateTime DateAttended { get; set; }
        public void AddAttendance()
        {
            var attend = new Attendance();
            attend.CandidateId = CandidateId;
            attend.SessionId=SelectedSession.SessionId;
            attend.DateAttended = DateAttended;
            attend.SessionDescription = SessionDescription;
            
            CandidateAttendances.Add(new CandidateAttendance(attend));
        }
        public string JobTitle { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime DateEnded { get; set; }
        public string JobHistoryDescription { get; set; }
        public string CompanyName { get; set; }
        public void AddJobHistory()
        {
            var hist = new JobHistory();
            hist.CandidateId = CandidateId;
            hist.JobTitle = JobTitle;
            hist.Description = JobHistoryDescription;
            hist.DateAssigned = DateAssigned;
            hist.DateEnded = DateEnded;
            hist.CompanyName = CompanyName;

            CandidateJobHistories.Add(new CandidateJobHistory(hist));
        }

        public string ErrorsInText { get; set; }

        public virtual void SaveCandidate()
        {

            bool isValid = Validate();

            if (isValid)
            {
                var cand = new Candidate();
                cand.CandidateId = CandidateId;
                cand.CandidateName = CandidateName;
                cand.HomeAddress = HomeAddress;
                cand.WorkAddress = WorkAddress;
                cand.EmailAddress = EmailAddress;
                cand.TelephoneNumber = PhoneNumber;

                //for certificates

                cand.Certificates = new List<Certificate>();

                foreach (var i in CandidateCertificates)
                {
                    cand.Certificates.Add( new Certificate
                    {
                        QualificationId = i.QualificationId,
                        CandidateId = cand.CandidateId,
                        Description = i.Description,
                        DateEarned = i.DateEarned

                    });
                    
                }

                //for sessions attended
                cand.Attendances = new List<Attendance>();

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
                cand.JobHistories = new List<JobHistory>();

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
                    _context.Add(cand);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message);
                }
            }

        }

        public void CreateNewQualification()
        {
            var newQual = new AddQualificationViewModel(_context);

            var window = new AddQualificationWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newQual;

            window.Show();
        }

        private bool Validate()
        {
            var validator = new AddCandidateDtoValidator();

            var result=validator.Validate(this);

            var sb=new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }

            ErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(ErrorsInText));

            return result.IsValid;


        }
    }


    public class AddCandidateDtoValidator:AbstractValidator<AddCandidateViewModel>
    {
        public AddCandidateDtoValidator()
        {
            RuleFor(c => c.CandidateName).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.HomeAddress).NotEmpty();
            RuleFor(c => c.EmailAddress).NotEmpty();
            RuleFor(c => c.WorkAddress).NotEmpty();
            RuleFor(c => c.SelectedQualification).NotNull();

            RuleForEach(c => c.CandidateCertificates).NotNull()
                .WithMessage("A candidate must have at least one qualification");
        }
    }

    public class CandidateCertificateValidator : AbstractValidator<CandidateCertificate>
    {
        public CandidateCertificateValidator()
        {
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.DateEarned).NotNull();
        }
    }
}
