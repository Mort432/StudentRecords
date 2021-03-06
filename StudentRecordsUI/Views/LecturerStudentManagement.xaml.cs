﻿using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        //Fetch the view model from Autofac.
        public LecturerStudentManagementViewModel viewModel = App._container.Resolve<LecturerStudentManagementViewModel>();

        //When lecturer changes which student they're inspecting
        private void lecturerStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Fetch the new student
            if ((sender as ListView).SelectedItem == null) return;
            User selectedUser = (sender as ListView).SelectedItem as User;

            //Apply it to the view
            viewModel.selectedStudent = selectedUser;

            this.Bindings.Update();
        }

        //If graduated checkbox checked
        private void studentGraduatedCheckBox_Graduate(object sender, RoutedEventArgs e)
        {
            //Graduate user
            viewModel.UpdateUserGraduation(true);
            this.Bindings.Update();
        }

        //If graduated checkbox unchecked
        private void studentGraduatedCheckBox_Ungraduate(object sender, RoutedEventArgs e)
        {
            //Ungraduate user
            viewModel.UpdateUserGraduation(false);
            this.Bindings.Update();
        }

        //If apply grade is clicked
        private async void gradingButton_Clicked(object sender, RoutedEventArgs e)
        {
            //Get assignment
            var item = (sender as FrameworkElement).Tag as Assignment;
            string gradeInput = await InputGradeDialog();

            //Fetch lecturer input from dialog box and apply it
            viewModel.NewResult(item, gradeInput);

            this.Bindings.Update();
        }

        //Grade input dialog
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
