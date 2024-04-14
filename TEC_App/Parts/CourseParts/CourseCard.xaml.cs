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
using TemporaryEmployeeCorporation_1.Core;

namespace TEC_App.Parts.CourseParts
{
    /// <summary>
    /// Interaction logic for CourseCard.xaml
    /// </summary>
    public partial class CourseCard : UserControl
    {
        private CourseViewModel _courseViewModel;
        public CourseCard()
        {
            InitializeComponent();
        }

        private void CourseCard_OnLoaded(object sender, RoutedEventArgs e)
        {
            _courseViewModel =DataContext as CourseViewModel;
        }
    }
}
