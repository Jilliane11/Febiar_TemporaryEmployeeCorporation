using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddCompanyViewModel:INotifyPropertyChanged
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
        public string ErrorsInText { get; set; }
        public AddCompanyViewModel(TECContext context)
        {
            _context = context;

        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string CompanyEmail { get; set; }

        public void AddCompany()
        {
            bool isValid = Validate();
            if (isValid)
            {
                var comp = new Company();
                comp.CompanyId = CompanyId;
                comp.CompanyName = CompanyName;
                comp.Address = Address;
                comp.TelephoneNumber = TelephoneNumber;
                comp.CompanyEmail = CompanyEmail;

                try
                {
                    _context.Add(comp);
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
            var validator = new NewCompanyDtoValidator();

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

    public class NewCompanyDtoValidator : AbstractValidator<AddCompanyViewModel>
    {
        public NewCompanyDtoValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.TelephoneNumber).NotEmpty();
        }
    }
}
