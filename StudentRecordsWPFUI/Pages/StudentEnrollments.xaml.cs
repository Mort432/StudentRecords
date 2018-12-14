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

        //Inject ViewModel from Autofac
        private StudentEnrollmentsViewModel viewModel = App.Container.Resolve<StudentEnrollmentsViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = viewModel;

            base.OnInitialized(e);
        }

        //If the user clicks to enroll
        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the module
            var item = (sender as FrameworkElement).Tag as Identifier;

            //Enroll them
            viewModel.EnrollUserToModule(item);

            //Refresh the bindings
            DataContext = null;
            DataContext = viewModel;
        }

        //If the user clicks to unenroll
        private void unenrollButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the module
            var item = (sender as FrameworkElement).Tag as Identifier;

            //Unenroll them
            viewModel.UnenrollUserFromModule(item);

            //Refresh the bindings
            DataContext = null;
            DataContext = viewModel;
        }

        //If the user changes the course combo box
        private void courseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the course.
            //Make sure to ignore this event if it was triggered by the pageload.
            if (!courseComboBox.IsLoaded) return;
            var item = ((sender as ComboBox).SelectedItem as Identifier);
            if (item == null)
            {
                return;
            }

            //Change to that course.
            viewModel.ChangeUserCourse(item);
        }
    }
}
