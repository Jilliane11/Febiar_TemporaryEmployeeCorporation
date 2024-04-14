using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation;
using TEC_App.Parts;
using TEC_App.Parts.CompanyParts;
using TEC_App.Parts.QualificationParts;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddOpeningViewModel:INotifyPropertyChanged
    {
        #region INotifyProprtyChanged
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

        protected TECContext _context;
        private string _searchCompany;
        private string _searchQualification;
        public AddOpeningViewModel(TECContext context)
        {
            _context=context;

            SearchCompany=string.Empty;
            SearchQualification = string.Empty;
            FilterCompanies();
            FilterQualifications();
        }

        public int OpeningId { get; set; }
        public string OpeningDescription { get; set; }
        public double HourlyPay { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Company SelectedCompany { get; set; }
        public ObservableCollection<Company> Companies { get; set; } = new();
        
        public Qualification SelectedQualification { get; set; }

        public ObservableCollection<Qualification> Qualifications { get; set; }=new();

        public string SearchCompany
        {
            get=> _searchCompany;
            set
            {
                _searchCompany=value;
                FilterCompanies();

                SelectedCompany=Companies.Count > 0 ?Companies[0] : null;

                OnPropertyChanged(nameof(SelectedCompany));

            }
        }

        public string SearchQualification
        {

            get=> _searchQualification;
            set
            {
                _searchQualification=value;
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

        private void FilterCompanies()
        {
            Companies.Clear();

            var companies = _context.Companies
                .Where(c => c.CompanyName.ToLower().Contains(SearchCompany))
                .Take(10)
                .ToList();

            foreach (var i in companies)
            {
                Companies.Add(i);
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

        public void CreateNewCompany()
        {

            var newComp=new AddCompanyViewModel(_context);

            var window = new AddCompanyWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newComp;
            window.Show();
        }


        public virtual void SaveOpening()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var opening = new Opening();
                opening.OpeningId = OpeningId;
                opening.OpeningDescription = OpeningDescription;
                opening.HourlyPay = HourlyPay;
                opening.DateStart=DateStart;
                opening.DateEnd = DateEnd;
                //one company
                opening.CompanyId = SelectedCompany.CompanyId;
                //one qualification
                opening.QualificationId=SelectedQualification.QualificationId;

                try
                {
                    _context.Add(opening);
                    _context.SaveChanges();
                    OnPropertyChanged();
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
            var validator = new AddOpeningDtoValidator();

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

    public class AddOpeningDtoValidator:AbstractValidator<AddOpeningViewModel>
    {
        public AddOpeningDtoValidator()
        {
            RuleFor(c => c.HourlyPay).GreaterThan(0);
            RuleFor(c => c.DateStart).Must(c => c.Date >= DateTime.Now);
            RuleFor(c => c.DateEnd).Must(c => c.Date >= DateTime.Now);
        }
    }
}
