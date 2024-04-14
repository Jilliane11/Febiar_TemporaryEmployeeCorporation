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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TEC_App.ViewModels;
using TemporaryEmployeeCorporation_1;

namespace TEC_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CandidateViewModel _candidateViewModel;
        private OpeningViewModel _openingViewModel;
        private AddQualificationViewModel _addQualificationViewModel;
        private TECContext _context;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context=new TECContext();
            _candidateViewModel = new CandidateViewModel(context);
            DataContext = _candidateViewModel;

            _addQualificationViewModel = new AddQualificationViewModel(context);
            DataContext=_addQualificationViewModel;

            _openingViewModel=new OpeningViewModel(context);
            DataContext= _openingViewModel;

            _openingViewModel.LoadOpenings();

            _candidateViewModel.LoadCandidates();
        }

        private void BtnShowCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
