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
    /// Interaction logic for AddCandidateWindow.xaml
    /// </summary>
    public partial class AddCandidateWindow : Window
    {
        public AddCandidateWindow()
        {
            InitializeComponent();

        }

        
        
        private AddCandidateViewModel _addCandidateViewModel;
        private CandidateViewModel _candidateViewModel;
        private void AddCandidateWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();
            _addCandidateViewModel = new AddCandidateViewModel(context);

            DataContext = _addCandidateViewModel;
            
            DtPkrDateEarned.DisplayDate=DateTime.Now;
            DtPkrDateEarned.SelectedDate=DateTime.Now;

            DtPkrDateAttended.DisplayDate = DateTime.Now;
            DtPkrDateAttended.SelectedDate = DateTime.Now;

        }

        private void BtnAddCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            _addCandidateViewModel.SaveCandidate();

            MessageBox.Show("Candidate Successfully Added");
            
            var parent=Window.GetWindow(this);

            parent.Close();
        }
        private void BtnAddQualification_OnClick(object sender, RoutedEventArgs e)
        {
            _addCandidateViewModel.CreateNewQualification();
        }
        

        #region AddButtons
        
        private void BtnAddCertificate_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddCandidateViewModel;
            context.AddCertificate();
        }

        private void BtnAddAttendance_OnClick(object sender, RoutedEventArgs e)
        {
            var context=DataContext as AddCandidateViewModel;
            context.AddAttendance();
        }
        private void BtnAddJobHistory_OnClick(object sender, RoutedEventArgs e)
        {
            var context =DataContext as AddCandidateViewModel;
            context.AddJobHistory();
        }


        #endregion

        #region Search
        private void TxtSearch_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void TxtSearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtSearch.Text))
                TblSearchBackground.Visibility = Visibility.Hidden;
            else
                TblSearchBackground.Visibility = Visibility.Visible;
        }
        private void TxtSearchAttendance_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TblSearchBackgroundAttendance.Visibility = Visibility.Hidden;
        }

        private void TxtSearchAttendance_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtSearchAttendance.Text))
                TblSearchBackgroundAttendance.Visibility = Visibility.Hidden;
            else
                TblSearchBackgroundAttendance.Visibility = Visibility.Visible;
        }
        

        #endregion


        private void BtnCancelCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Close();
        }
        
    }
}
