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

namespace TEC_App.Parts.OpeningParts
{
    /// <summary>
    /// Interaction logic for OpeningCard.xaml
    /// </summary>
    public partial class OpeningCard : UserControl
    {
        private OpeningViewModel _openingViewModel;
        private AddOpeningViewModel _addingViewModel;
        public OpeningCard()
        {
            InitializeComponent();
        }

        private void OpeningCard_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            _openingViewModel = DataContext as OpeningViewModel;
            
            
        }
    }
}
