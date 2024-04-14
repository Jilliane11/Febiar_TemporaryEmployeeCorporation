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
using TEC_App.Helpers;
using TEC_App.ViewModels;
using TemporaryEmployeeCorporation_1;

namespace TEC_App.Parts.OpeningParts
{
    /// <summary>
    /// Interaction logic for OpeningList.xaml
    /// </summary>
    public partial class OpeningList : UserControl
    {
        public OpeningList()
        {
            InitializeComponent();
        }

        private void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.PrevPage();
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.NextPage();
        }

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

        private Pagination _pageContext;
        
        private void OpeningList_OnLoaded(object sender, RoutedEventArgs e)
        {
            var s = DataContext as OpeningViewModel;

          _pageContext = s.PageDetails;
        }
    }
}
