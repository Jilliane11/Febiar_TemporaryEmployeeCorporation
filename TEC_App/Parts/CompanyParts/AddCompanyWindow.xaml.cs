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

namespace TEC_App.Parts.CompanyParts
{
    /// <summary>
    /// Interaction logic for AddCompanyWindow.xaml
    /// </summary>
    public partial class AddCompanyWindow : Window
    {
        private TECContext _context;
        public AddCompanyWindow()
        {
            InitializeComponent();
        }

        private void BtnAddCompany_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddCompanyViewModel;
            context.AddCompany();


            MessageBox.Show("New Company Successfully Added");

            var parent = Window.GetWindow(this);

            parent.Close();
        }

        private void BtnCancelCompany_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private AddCompanyViewModel _addCompanyViewModel;
        private void AddCompanyWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();
            _addCompanyViewModel = new AddCompanyViewModel(context);
            DataContext = _addCompanyViewModel;

            DataContext = _addCompanyViewModel;
        }
    }
}
