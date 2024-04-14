    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TEC_App.Dto;
using TEC_App.Helpers;
using TEC_App.Parts;
using TEC_App.Parts.QualificationParts;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.ViewModels
{
    public class CandidateViewModel:INotifyPropertyChanged
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

        private TECContext _context;
        private Visibility _bookDetailsVisibility = Visibility.Collapsed;
        private string _searchText;
        private CandidateNameDto _selectedCandidate;


        #endregion

        #region Properties
        public ObservableCollection<CandidateNameDto> Candidates { get; } = new();
        public Pagination PageDetails { get; private set; }
        public CandidateDetails SelectedCandidateDetails { get; set; }
        public CandidateNameDto SelectedCandidate
        {
            get => _selectedCandidate;
            set
            {
                _selectedCandidate = value;
                OnPropertyChanged();
                LoadCandidateDetails();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterCandidates();
            }
        }
        

        #endregion

        #region Constructors
        public CandidateViewModel(TECContext context):this()
        {
            _context = context;


        }
        private CandidateViewModel()
        {
            PageDetails = new Pagination(FilterCandidates);
        }

        #endregion

        #region Methods
        public void LoadCandidates()
        {
            FilterCandidates();

        }

        private void FilterCandidates()
        {
            string search = SearchText?.ToLowerInvariant().Trim() ?? string.Empty;

            var query = _context.Candidates
                .Where(c => c.CandidateName.ToLower().Contains(search) ||
                            c.WorkAddress.ToLower().Contains(search)||
                            c.HomeAddress.ToLower().Contains(search));

            int totalPages=query.Count();
            UpdateTotalPages(totalPages);

            var candidates=query
                .OrderBy(c=>c.CandidateName)
                .Select(c=> new CandidateNameDto(c.CandidateId,c.CandidateName,c.HomeAddress,c.WorkAddress))
                .Skip(PageDetails.ItemsPerPage * (PageDetails.CurrentPage - 1))
                .Take(PageDetails.ItemsPerPage)
                .ToList();

            Candidates.Clear();

            foreach (var candidate in candidates)
                Candidates.Add(candidate);
            
        }

        private void UpdateTotalPages(int totalPages)
        {
            PageDetails.TotalPages = totalPages;
            //PageDetails.TotalPages = (int)Math.Ceiling((float)totalCount / PageDetails.ItemsPerPage);

            OnPropertyChanged(nameof(PageDetails));
        }

        private void LoadCandidateDetails()
        {
            if (_selectedCandidate==null) return;

            int candidateId = _selectedCandidate.CandidateId;

            var candidate = _context.Candidates
                .Include(c => c.JobHistories)
                .Include(c => c.Certificates)
                .ThenInclude(c=>c.QualificationLink)
                .Include(c => c.Attendances)!
                .ThenInclude(c=>c.SessionLink)
                .ThenInclude(c=>c.CourseLink)
                .Include(c => c.Placements)
                .Single(c => c.CandidateId == candidateId);

            SelectedCandidateDetails= new CandidateDetails(candidate);

            OnPropertyChanged(nameof(SelectedCandidateDetails));
        }
        


        public void CreateNewCandidate()
        {

            var newCand = new AddCandidateViewModel(_context);

            var window = new AddCandidateWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newCand;

            window.Show();
        }

        public void RemoveCandidate()
        {
            if (SelectedCandidate is null) return;

            var cand = _context.Candidates.First(c => c.CandidateId == SelectedCandidate.CandidateId);

            cand.Certificates.Clear();
            cand.Attendances.Clear();
            cand.JobHistories.Clear();
            cand.Placements.Clear();
            try
            {
                _context.Remove(cand);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            Candidates.Remove(SelectedCandidate);
            SelectedCandidate = null;
            SelectedCandidateDetails = null;

        }

        #endregion



        public void EditCandidate()
        {
            if (_selectedCandidate is null) return;

            var newCand = new EditCandidateViewModel(_context, SelectedCandidateDetails);

            var window = new EditCandidateWindow();

            window.Owner = Application.Current.MainWindow;

            window.DataContext = newCand;

            window.Show();

        }




    }
}
