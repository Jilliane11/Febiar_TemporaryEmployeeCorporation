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
using FluentValidation.TestHelper;
using TEC_App.ViewModels;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.Parts
{
    /// <summary>
    /// Interaction logic for OpeningUC.xaml
    /// </summary>
    public partial class OpeningUC : UserControl
    {
        private OpeningViewModel _openingViewModel;
        private AddOpeningViewModel _addOpeningViewModel;
        public OpeningUC()
        {
            InitializeComponent();
        }

        
        private void OpeningUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            var context = new TECContext();

            _openingViewModel = new OpeningViewModel(context);
            DataContext = _openingViewModel;
           

            cardOpeningDetails.Visibility = Visibility.Collapsed;

            _openingViewModel.PropertyChanged += ContextOnPropertyChanged;

            _openingViewModel.LoadOpenings();

        }

        private void ContextOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_openingViewModel.SelectedOpening))
            {
                if (_openingViewModel.SelectedOpening == null)
                    cardOpeningDetails.Visibility = Visibility.Collapsed;
                else
                    cardOpeningDetails.Visibility = Visibility.Visible;
            }
        }
   
        

        private void BtnAddOpening_OnClick(object sender, RoutedEventArgs e)
        {
            _openingViewModel.CreateNewOpening();
        }

        private void BtnRemoveOpening_OnClick(object sender, RoutedEventArgs e)
        {
            _openingViewModel.RemoveOpening();
        }
    }
}
