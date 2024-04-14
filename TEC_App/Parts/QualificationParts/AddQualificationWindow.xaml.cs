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

namespace TEC_App.Parts.QualificationParts
{
    /// <summary>
    /// Interaction logic for AddQualificationWindow.xaml
    /// </summary>
    public partial class AddQualificationWindow : Window
    {
        private TECContext _context;
        public AddQualificationWindow()
        {
            InitializeComponent();
        }

        private void BtnAddQualification_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as AddQualificationViewModel;
            context.AddQualification();


            MessageBox.Show("New Qualification Successfully Added");

            var parent = Window.GetWindow(this);

            parent.Close();
        }

        private void BtnCancelQualification_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private AddQualificationViewModel _addCandidateViewModel;
        private void AddQualificationWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();
            _addCandidateViewModel = new AddQualificationViewModel(context);
            DataContext = _addCandidateViewModel;
            
            DataContext = _addCandidateViewModel;

        }
    }
}
