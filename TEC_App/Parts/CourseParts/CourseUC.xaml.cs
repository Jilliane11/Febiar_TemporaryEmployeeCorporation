using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TEC_App.ViewModels;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.Parts.CourseParts
{
    /// <summary>
    /// Interaction logic for CourseUC.xaml
    /// </summary>
    public partial class CourseUC : UserControl
    {
        private CourseViewModel _courseViewModel;
        private TECContext _context;
        public CourseUC()
        {
            InitializeComponent();
        }

        private void CourseUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();

            _courseViewModel = new CourseViewModel(context);
            DataContext = _courseViewModel;

            cardCourseDetails.Visibility = Visibility.Collapsed;

            _courseViewModel.PropertyChanged += ContextOnPropertyChanged;

            _courseViewModel.LoadCourses();
        }
        
        private void ContextOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(_courseViewModel.SelectedCourse))
                {
                    if (_courseViewModel.SelectedCourse == null)
                        cardCourseDetails.Visibility = Visibility.Collapsed;
                    else
                        cardCourseDetails.Visibility = Visibility.Visible;
                }
            }

        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            _courseViewModel.CreateNewCourse();
        }

        private void BtnRemoveCourse_OnClick(object sender, RoutedEventArgs e)
        {
            _courseViewModel.RemoveCourse();
        }
    }
}
