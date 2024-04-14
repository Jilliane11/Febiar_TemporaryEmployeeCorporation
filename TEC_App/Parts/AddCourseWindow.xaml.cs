using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TEC_App.ViewModels;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.Parts
{
    /// <summary>
    /// Interaction logic for AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        public AddCourseWindow()
        {
            InitializeComponent();
        }

        private void TxtQualificationSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            QualificationTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void TxtQualificationSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtQualificationSearch.Text))
                QualificationTblSearchBackground.Visibility = Visibility.Hidden;
            else
                QualificationTblSearchBackground.Visibility = Visibility.Visible;
        }

        private AddCourseViewModel _addCourseViewModel;
        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            var context=DataContext as AddCourseViewModel;
            _addCourseViewModel.SaveCourse();

            MessageBox.Show("Course Successfully Added");

            var parent = Window.GetWindow(this);

            parent.Close();
        }
        
        private void AddCourseWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();
            _addCourseViewModel = new AddCourseViewModel(context);
            DataContext = _addCourseViewModel; 

        }
        
        private void BtnAddQualification_OnClick(object sender, RoutedEventArgs e)
        {
            _addCourseViewModel.CreateNewQualification();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Close();
        }
        

        private void BtnAddRequirement_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddCourseViewModel;
            context.AddRequirement();
        }

        private void BtnAddSession_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddCourseViewModel;
            context.AddSession();
        }
    }
}
