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
using StudentRecordsWPFUI.Controls;

namespace StudentRecordsWPFUI.Pages
{
    /// <summary>
    /// Interaction logic for LecturerStudentManagement.xaml
    /// </summary>
    public partial class LecturerStudentManagement : Page
    {
        public LecturerStudentManagement()
        {
            InitializeComponent();
        }

        //Inject ViewModel from Autofac
        private LecturerStudentManagementViewModel viewModel = App.Container.Resolve<LecturerStudentManagementViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = viewModel;

            base.OnInitialized(e);
        }

        //If the grading button is clicked on a student's assignment
        private void gradingButton_Clicked(object sender, RoutedEventArgs e)
        {
            //Get the assignment
            var item = (sender as FrameworkElement).Tag as Assignment;
            string gradeInput = "";

            //Show the grade dialog and fetch the result
            GradeDialog gradeDialog = new GradeDialog("Please assign new grade (leave blank to unassign)");
            if (gradeDialog.ShowDialog() == true)
            {
                gradeInput = gradeDialog.Answer;
            }

            //Submit the new result
            viewModel.NewResult(item, gradeInput);

            RefreshBindings();
        }

        //If the graduation checkbox is checked
        private void studentGraduatedCheckBox_Graduate(object sender, RoutedEventArgs e)
        {
            //Graduate the user
            viewModel.UpdateUserGraduation(true);
        }

        //If the graduation checkbox is unchecked
        private void studentGraduatedCheckBox_Ungraduate(object sender, RoutedEventArgs e)
        {
            //Ungraduate the user
            viewModel.UpdateUserGraduation(false);
        }

        //If the selected student is changed
        private void lecturerStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the new student, then assign them in the view model
            if ((sender as ListView).SelectedItem == null) return;
            User selectedUser = (sender as ListView).SelectedItem as User;
            viewModel.selectedStudent = selectedUser;

            RefreshBindings();
        }

        private void RefreshBindings()
        {
            DataContext = null;
            DataContext = viewModel;
        }
    }
}
