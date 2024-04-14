using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TEC_App.Dto;
using TEC_App.Helpers;
using TEC_App.Parts;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.ViewModels
{
    public class OpeningViewModel:INotifyPropertyChanged
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
        private Visibility _openingDetailsVisibility = Visibility.Collapsed;
        private string _searchtext;
        private OpeningDescriptionDto _selectedOpening;

        public Pagination PageDetails { get; private set; }
        public ObservableCollection<OpeningDescriptionDto> Openings { get; } = new();
        public OpeningDetails SelectedOpeningDetails { get; set; }
        public OpeningViewModel(TECContext context):this()
        {
            _context=context;

        }
        public OpeningViewModel()
        {
            PageDetails = new Pagination(FilterOpenings);
        }

        public OpeningDescriptionDto SelectedOpening
        {
            get=> _selectedOpening;
            set
            {
                _selectedOpening=value;
                OnPropertyChanged();
                LoadOpeningDetails();
            }
        }
        public string SearchText
        {
            get => _searchtext;
            set
            {
                _searchtext=value;
                OnPropertyChanged();

                FilterOpenings();
            }
        }
        

        public void LoadOpenings()
        {
            FilterOpenings();
        }
        private void FilterOpenings()
        {
            string search=SearchText?.ToLowerInvariant() ?? string.Empty;

            var query = _context.Openings
                .Where(c => c.OpeningDescription.ToLower().Contains(search) ||
                            c.QualificationLink.Code.ToLower().Contains(search) ||
                            c.CompanyLink.CompanyName.ToLower().Contains(search));

            int totalPages = query.Count();
            UpdateTotalPages(totalPages);

            var openings=query
                .OrderBy(c=>c.OpeningDescription)
                .Select(c=> new OpeningDescriptionDto(c.OpeningId,c.OpeningDescription))
                .Skip(PageDetails.ItemsPerPage * (PageDetails.CurrentPage-1))
                .Take(PageDetails.ItemsPerPage)
                .ToList();

            Openings.Clear();

            foreach (var opening in openings)
            {
                Openings.Add(opening);
            }




        }

        private void UpdateTotalPages(int totalPages)
        {
            PageDetails.TotalPages = totalPages;
            //PageDetails.TotalPages = (int)Math.Ceiling((float)totalCount / PageDetails.ItemsPerPage);

            OnPropertyChanged(nameof(PageDetails));
        }

        private void LoadOpeningDetails()
        {
            if (_selectedOpening is null) return;

            int openingId = _selectedOpening.OpeningId;

            var opening = _context.Openings
                .Include(c => c.Placements)?
                .ThenInclude(c => c.CandidateLink)
                .Include(c => c.QualificationLink)
                .Include(c => c.CompanyLink)
                .Single(c => c.OpeningId == openingId);

            SelectedOpeningDetails = new OpeningDetails(opening);

            OnPropertyChanged(nameof(SelectedOpeningDetails));
        }

        public void CreateNewOpening()
        {
            var newOp = new AddOpeningViewModel(_context);

            var window = new AddOpeningWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newOp;
            window.Show();
        }

        public void RemoveOpening()
        {
            if (SelectedOpening is null) return;

            var op = _context.Openings.First(c => c.OpeningId == SelectedOpening.OpeningId);

            op.CompanyLink = null;
            op.Placements.Clear();
            op.QualificationLink = null;
            try
            {
                _context.Remove(op);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            Openings.Remove(SelectedOpening);
            SelectedOpening = null;
            SelectedOpeningDetails = null;
            

        }
    }
}
