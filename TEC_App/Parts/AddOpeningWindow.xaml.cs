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
    /// Interaction logic for AddOpeningWindow.xaml
    /// </summary>
    public partial class AddOpeningWindow : Window
    {
        public AddOpeningWindow()
        {
            InitializeComponent();
        }

        private AddOpeningViewModel _addOpeningViewModel;

        private void TxtCompanySearch_OnGotFocus(object sender, RoutedEventArgs e)
        {

            CompanyTblSearchBackground.Visibility = Visibility.Hidden;
        }

        private void TxtCompanySearch_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCompanySearch.Text))
                CompanyTblSearchBackground.Visibility = Visibility.Hidden;
            else
               CompanyTblSearchBackground.Visibility = Visibility.Visible;
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

        private void BtnAddQualification_OnClick(object sender, RoutedEventArgs e)
        {
            _addOpeningViewModel.CreateNewQualification();
        }
        
        private void BtnAddCompany_OnClick(object sender, RoutedEventArgs e)
        {
            _addOpeningViewModel.CreateNewCompany();
        }

        private void BtnAddOpening_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddOpeningViewModel;
            context.SaveOpening();
            MessageBox.Show("Opening Successfully Added");

            var parent = Window.GetWindow(this);

            parent.Close();
        }

        private OpeningViewModel _context;
        private void AddOpeningWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();
            _addOpeningViewModel = new AddOpeningViewModel(context);
            DataContext = _addOpeningViewModel;

            DataContext = _addOpeningViewModel;
        }


        private void BtnCancelOpening_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
