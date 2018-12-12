using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentRecordsUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LecturerStudentManagement : Page
    {
        public LecturerStudentManagement()
        {
            this.InitializeComponent();
        }

        public LecturerStudentManagementViewModel viewModel = App._container.Resolve<LecturerStudentManagementViewModel>();

        private void lecturerStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;
            User selectedUser = (sender as ListView).SelectedItem as User;
            viewModel.selectedStudent = selectedUser;

            this.Bindings.Update();
        }

        private void studentGraduatedCheckBox_Graduate(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateUserGraduation(true);
            this.Bindings.Update();
        }

        private void studentGraduatedCheckBox_Ungraduate(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateUserGraduation(false);
            this.Bindings.Update();
        }

        private async void gradingButton_Clicked(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Assignment;
            
            string gradeInput = await InputGradeDialog();
            int grade;
            if (gradeInput == "Cancel")
            {
                //Do nothing if cancelled
                return;
            }
            else if (string.IsNullOrEmpty(gradeInput))
            {
                if(item.Results.ElementAt(0) == null)
                {
                    //If there isn't a result yet, don't do anything.
                    return;
                }
                //If the submitted string was empty, delete their grade.
                viewModel.DeleteResult(item);
            }
            else if (int.TryParse(gradeInput, out grade)) //Ensure input parses to integer before proceeding
            {
                //Assign result given
                viewModel.AssignResult(item, grade);
            }
            this.Bindings.Update();
        }

        private async Task<string> InputGradeDialog()
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = "Input Grade";
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Assign";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "Cancel";
        }
    }
}
