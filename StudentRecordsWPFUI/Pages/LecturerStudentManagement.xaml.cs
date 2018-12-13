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

        private LecturerStudentManagementViewModel viewModel = App.Container.Resolve<LecturerStudentManagementViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = viewModel;

            base.OnInitialized(e);
        }

        private void gradingButton_Clicked(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Assignment;
            string gradeInput = "";

            GradeDialog gradeDialog = new GradeDialog("Please assign new grade (leave blank to unassign)");
            if (gradeDialog.ShowDialog() == true)
            {
                gradeInput = gradeDialog.Answer;
            }

            viewModel.NewResult(item, gradeInput);

            RefreshBindings();
        }

        private void studentGraduatedCheckBox_Graduate(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateUserGraduation(true);
        }

        private void studentGraduatedCheckBox_Ungraduate(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateUserGraduation(false);
        }

        private void lecturerStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
