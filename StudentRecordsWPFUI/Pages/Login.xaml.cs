using Autofac;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace StudentRecordsWPFUI.Pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        //Inject ViewModel from Autofac
        private LoginViewModel ViewModel = App.Container.Resolve<LoginViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }

        //If the login button is clicked
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Login with the selected user
            ViewModel.Login(ViewModel.AvailableUsers[UsersDropDown.SelectedIndex]);

            MainWindow.WindowFrame.Navigate(new Main());
        }
    }
}
