using Autofac;
using StudentRecordsViewModels.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentRecordsUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        LoginViewModel viewModel = App._container.Resolve<LoginViewModel>();

        private void loginButton_Clicked(object sender, RoutedEventArgs e)
        {
            viewModel.Login(viewModel.AvailableUsers[usersDropDown.SelectedIndex]);
            Frame.Navigate(typeof(MainPage));
        }
    }
}
