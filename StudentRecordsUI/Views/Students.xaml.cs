using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsUI.ViewModels;
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
    public sealed partial class Students : Page
    {
        public Students()
        {
            this.InitializeComponent();
        }

        StudentsViewModel viewModel = App._container.Resolve<StudentsViewModel>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void studentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UserSelected(studentsListView.SelectedIndex);
            this.Bindings.Update();
        }
    }
}
