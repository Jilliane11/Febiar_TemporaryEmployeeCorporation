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

namespace TEC_App.Parts.CourseParts
{
    /// <summary>
    /// Interaction logic for CourseList.xaml
    /// </summary>
    public partial class CourseList : UserControl
    {
        public Pagination _pageContext;
        public CourseList()
        {
            InitializeComponent();
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

        private void BtnPrev_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.PrevPage();
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            _pageContext.NextPage();
        }

        private void CourseList_OnLoaded(object sender, RoutedEventArgs e)
        {
            var s = DataContext as CourseViewModel;
            _pageContext = s.PageDetails;
        }
    }
}
