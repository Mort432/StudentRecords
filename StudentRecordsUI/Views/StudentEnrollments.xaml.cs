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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentRecordsUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentEnrollments : Page
    {
        public StudentEnrollments()
        {
            this.InitializeComponent();
        }

        StudentEnrollmentsViewModel viewModel = App._container.Resolve<StudentEnrollmentsViewModel>();

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Identifier;

            viewModel.EnrollUserToModule(item);
            this.Bindings.Update();
        }

        private void unenrollButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as Identifier;

            viewModel.UnenrollUserFromModule(item);
            this.Bindings.Update();
        }

        private void courseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((sender as ComboBox).SelectedItem as Identifier);

            viewModel.ChangeUserCourse(item);
        }
    }
}
