using Microsoft.EntityFrameworkCore;
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

namespace TEC_App.Parts.CandidateParts
{
    /// <summary>
    /// Interaction logic for CandidateCard.xaml
    /// </summary>
    public partial class CandidateCard : UserControl
    {
        private CandidateViewModel _candidateViewModel;
        public CandidateCard()
        {
            InitializeComponent();
        }
        

        private CandidateViewModel _context;
        private void CandidateCard_OnLoaded(object sender, RoutedEventArgs e)
        {


            _context = DataContext as CandidateViewModel;
            // var context = new TECContext();
            // _candidateViewModel = new CandidateViewModel(context);
            // DataContext = _candidateViewModel;

        }
    }
}
