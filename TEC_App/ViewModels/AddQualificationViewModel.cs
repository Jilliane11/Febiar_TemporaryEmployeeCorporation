using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using TemporaryEmployeeCorporation_1;
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.ViewModels
{
    public class AddQualificationViewModel:INotifyPropertyChanged
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

        public AddQualificationViewModel(TECContext context)
        {
            _context = context;

        }

        public int QualificationId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Specialization { get; set; }

        public void AddQualification()
        {
            bool isValid = Validate();

            if (isValid)
            {
                var qual = new Qualification();
                qual.QualificationId = QualificationId;
                qual.Code = Code;
                qual.Type = Type;
                qual.Specialization = Specialization;

                qual.Description = Type +","+ Specialization;


                try
                {
                    _context.Add(qual);
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
            var validator = new NewQualificationDtoValidator();

            var result=validator.Validate(this);

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


    public class NewQualificationDtoValidator : AbstractValidator<AddQualificationViewModel>
    {
        public NewQualificationDtoValidator()
        {
            RuleFor(c => c.Code).NotEmpty().WithMessage("Code is required and must be unique");
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.Specialization).NotEmpty();

        }
    }
}
