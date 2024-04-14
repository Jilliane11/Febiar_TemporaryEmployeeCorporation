using FluentValidation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore.Query;
using TEC_App.Dto;
using TEC_App.Parts.QualificationParts;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddCourseViewModel:INotifyPropertyChanged
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


        #endregion

        #region Properties

        public ObservableCollection<Qualification> Qualifications { get; set; } = new();
        public ObservableCollection<Requirement> Requirements { get; set; } = new();
        
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

        #endregion

        #region Constructors

        public AddCourseViewModel(TECContext context)
        {
            _context=context;
            SearchQualification = string.Empty;
            FilterQualifications();

        }

        #endregion

       

        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get;set; }
        public string CourseDescription { get; set; }
        public Qualification SelectedQualification { get; set; }

        public ObservableCollection<CourseSession> CourseSessions { get; set; } = new();

        public ObservableCollection<CourseRequirement> CourseRequirements { get; set; } = new();
        //sessions
        public string SessionName { get; set; }
        public string SessionDescription { get; set; }
        public int Duration { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public float SessionFee { get; set; }

        public void AddSession()
        {
            var session = new Session();
            session.CourseId = CourseId;
            session.SessionName = SessionName;
            session.SessionDescription = SessionDescription;
            session.SessionFee = SessionFee;
            session.Duration = Duration;
            session.DateStart = DateStart;
            session.DateEnd = DateEnd;
            session.SessionFee = SessionFee;

            CourseSessions.Add(new CourseSession(session));

        }

        public string RequirementDescription { get; set; }

        public void AddRequirement()
        {
            var req = new Requirement();
            req.CourseId= CourseId;
            req.QualificationId = SelectedQualification.QualificationId;
            req.RequirementDescription = RequirementDescription;
            
            
            CourseRequirements.Add(new CourseRequirement(req));
            
        }

        public void CreateNewQualification()
        {
            var newQual = new AddQualificationViewModel(_context);

            var window = new AddQualificationWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newQual;
            window.Show();
        }

        public virtual void SaveCourse()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var course = new Course();
                course.CourseId = CourseId;
                course.CourseCode = CourseCode;
                course.CourseName = CourseName;
                course.CourseDescription = CourseDescription;

                //for Requirements
                course.Requirements = new List<Requirement>();

                foreach (var i in CourseRequirements)
                {
                    course.Requirements.Add(new Requirement
                    {
                        CourseId = course.CourseId,
                        QualificationId = i.QualificationId,
                        RequirementDescription = i.RequirementDescription,
                    });
                }
                
                //for Sessions

                course.Sessions = new List<Session>();

                foreach (var i in CourseSessions)
                {
                    course.Sessions.Add(new Session
                    {
                        CourseId = course.CourseId,
                        SessionName = i.SessionName,
                        SessionDescription = i.SessionDescription,
                        SessionFee = i.SessionFee,
                        Duration = i.Duration,
                        DateStart = i.DateStart,
                        DateEnd = i.DateEnd,

                    });
                }

                try
                {
                    _context.Add(course);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message);
                }
            }





        }
        public string ErrorsInText { get; set; }
        private bool Validate()
        {
            var validator = new AddCourseDtoValidator();

            var result = validator.Validate(this);

            var sb = new StringBuilder();

            foreach (var error in result.Errors)
            {
                sb.AppendLine(error.ErrorMessage);
            }

            ErrorsInText = sb.ToString();

            OnPropertyChanged(nameof(ErrorsInText));

            return result.IsValid;


        }

    }

    public class AddCourseDtoValidator:AbstractValidator<AddCourseViewModel>
    {
        public AddCourseDtoValidator()
        {
            RuleFor(c => c.CourseName).NotEmpty();
            RuleFor(c => c.CourseCode).NotEmpty();
            RuleFor(c=>c.CourseDescription).NotEmpty();
        }
    }
}
