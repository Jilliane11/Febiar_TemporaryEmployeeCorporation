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
using static System.Reflection.Metadata.BlobBuilder;

namespace TEC_App.ViewModels
{
    public class CourseViewModel:INotifyPropertyChanged
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
        private Visibility _courseDetailsVisibility = Visibility.Collapsed;
        private string _searchText { get; set; }
        private CourseNameDto _selectedCourse;
        

        #endregion

        #region Properties
        public ObservableCollection<CourseNameDto> Courses { get; set; } = new();
        public Pagination PageDetails { get; private set; }
        
        public CourseDetails SelectedCourseDetails { get; set; }

        public CourseNameDto SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse=value;
                OnPropertyChanged();
                LoadCourseDetails();
            }

        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText=value;
                OnPropertyChanged();
                FilterCourses();
            }
        }

        #endregion

        #region Constructors
        public CourseViewModel(TECContext context):this()
        {
            _context=context;
        }

        public CourseViewModel()
        {
            PageDetails = new Pagination(FilterCourses);
        }
        #endregion

        #region Methods

        public void LoadCourses()
        {
            FilterCourses();
        }
        private void LoadCourseDetails()
        {
            if (_selectedCourse==null) return;

            int courseId = _selectedCourse.CourseId;

            var course = _context.Courses
                .Include(c => c.PrerequisiteCourseLink)
                .Include(c => c.Requirements)
                .Include(c => c.Sessions)
                .Single(c => c.CourseId == courseId);

            SelectedCourseDetails = new CourseDetails(course);

            OnPropertyChanged(nameof(SelectedCourseDetails));
        }
        public void FilterCourses()
        {
            string search = SearchText?.ToLowerInvariant().Trim() ?? string.Empty;

            var query = _context.Courses
                .Where(c => c.CourseName.ToLower().Contains(search) ||
                            c.CourseDescription.ToLower().Contains(search));

            int totalPages = query.Count();
            UpdateTotalPages(totalPages);

            var courses=query
                .OrderBy(c=>c.CourseName)
                .Select(c=>new CourseNameDto(c.CourseId,c.CourseCode,c.CourseName,c.CourseDescription))
                .Skip(PageDetails.ItemsPerPage * (PageDetails.CurrentPage - 1))
                .Take(PageDetails.ItemsPerPage)
                .ToList();

            Courses.Clear();

            foreach (var course in courses)
                Courses.Add(course);
            

        }

        private void UpdateTotalPages(int totalPages)
        {
            PageDetails.TotalPages = totalPages;
            //PageDetails.TotalPages = (int)Math.Ceiling((float)totalCount / PageDetails.ItemsPerPage);

            OnPropertyChanged(nameof(PageDetails));
        }

        public void CreateNewCourse()
        {
            var newCourse = new AddCourseViewModel(_context);

            var window = new AddCourseWindow();

            window.Owner = Application.Current.MainWindow;
            window.DataContext = newCourse;

            window.Show();
        }

        public void RemoveCourse()
        {
            if (SelectedCourse is null) return;

            var course = _context.Courses.First(c => c.CourseId == SelectedCourse.CourseId);

            course.Sessions.Clear();
            course.Requirements.Clear();
            try
            {
                _context.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            Courses.Remove(SelectedCourse);
            SelectedCourse = null;
            SelectedCourseDetails = null;
            
        }

        #endregion


    }
}
