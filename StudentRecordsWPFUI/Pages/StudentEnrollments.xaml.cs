using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsViewModels.ViewModels;
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

namespace StudentRecordsWPFUI.Pages
{
    /// <summary>
    /// Interaction logic for StudentEnrollments.xaml
    /// </summary>
    public partial class StudentEnrollments : Page
    {
        public StudentEnrollments()
        {
            InitializeComponent();
        }

        private StudentEnrollmentsViewModel viewModel = App.Container.Resolve<StudentEnrollmentsViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = viewModel;

            base.OnInitialized(e);
        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Identifier;

            viewModel.EnrollUserToModule(item);

            DataContext = null;
            DataContext = viewModel;
        }

        private void unenrollButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Identifier;

            viewModel.UnenrollUserFromModule(item);

            DataContext = null;
            DataContext = viewModel;
        }

        private void courseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((sender as ComboBox).SelectedItem as Identifier);

            viewModel.ChangeUserCourse(item);
        }
    }
}
