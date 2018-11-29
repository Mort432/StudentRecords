using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsUI;
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
    public sealed partial class Modules : Page
    {
        public Modules()
        {
            this.InitializeComponent();
        }

        ModulesViewModel viewModel = App._container.Resolve<ModulesViewModel>();

        private void usersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UserSelected(usersListView.SelectedIndex);
            this.Bindings.Update();
        }

        private void enrollButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).Tag as ModuleRun;

            item = item;
        }
    }
}
