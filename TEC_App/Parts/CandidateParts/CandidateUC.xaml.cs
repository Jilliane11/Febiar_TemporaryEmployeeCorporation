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

namespace TEC_App.Parts.CandidateParts
{
    /// <summary>
    /// Interaction logic for CandidateUC.xaml
    /// </summary>
    public partial class CandidateUC : UserControl
    {
        private CandidateViewModel _candidateViewModel;
        private TECContext _context;

        public CandidateUC()
        {
            InitializeComponent();
        }

        private void CandidateUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();

            _candidateViewModel=new CandidateViewModel(context);
            DataContext = _candidateViewModel;

            cardCandidateDetails.Visibility = Visibility.Collapsed;

            _candidateViewModel.PropertyChanged += ContextOnPropertyChanged;
            _candidateViewModel.LoadCandidates();
        }

        private void ContextOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_candidateViewModel.SelectedCandidate))
            {
                if (_candidateViewModel.SelectedCandidate == null)
                    cardCandidateDetails.Visibility = Visibility.Collapsed;
                else
                    cardCandidateDetails.Visibility = Visibility.Visible;
            }
        }

        private void BtnAddCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            _candidateViewModel.CreateNewCandidate();
        }

        private void BtnRemoveCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            _candidateViewModel.RemoveCandidate();
        }

        private void BtnEditCandidate_OnClick(object sender, RoutedEventArgs e)
        {
            _candidateViewModel.EditCandidate();
        }
    }
}
