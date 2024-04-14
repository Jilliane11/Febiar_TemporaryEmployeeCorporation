using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddSessionAttendanceViewModel:INotifyPropertyChanged
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

        private TECContext _context;
        private string _searchSession { get; set; }

        public AddSessionAttendanceViewModel(TECContext context)
        {
            _context = context;

            SearchSession = string.Empty;

            FilterSessions();
        }
        
        public DateTime DateAttended { get; set; }
        public Session SelectedSession { get; set; }
        public ObservableCollection<Session> Sessions { get; set; }

        public string SearchSession
        {
            get => _searchSession;
            set
            {
                _searchSession = value;
                FilterSessions();

                SelectedSession = Sessions.Count > 0 ? Sessions[0] : null;
                OnPropertyChanged(nameof(SelectedSession));
            }
        }

        private void FilterSessions()
        {
            Sessions.Clear();

            var sessions = _context.Sessions
                .Where(c => c.SessionName.ToLower().Contains(SearchSession))
                .Take(20)
                .ToList();

            foreach (var i in sessions)
            {
                Sessions.Add(i);
            }
        }

        public string ErrorsInText { get; set; }

        public void AddAttendance()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var attend = new Attendance();
                attend.SessionLink.SessionName = SelectedSession.SessionName;
                attend.DateAttended = DateAttended;
                try
                {
                    _context.Add(attend);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message);
                }

            }

        }

        private bool Validate()
        {
            var validator = new AddSessionAttendanceDtoValidator();

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

    public class AddSessionAttendanceDtoValidator : AbstractValidator<AddSessionAttendanceViewModel>
    {
        public AddSessionAttendanceDtoValidator()
        {
            RuleFor(c => c.DateAttended).Must(d => d.Date <= DateTime.Now);
            RuleFor(c => c.SelectedSession).NotEmpty();
            
        }
    }
}
