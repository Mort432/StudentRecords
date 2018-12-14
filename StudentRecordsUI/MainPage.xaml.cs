using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsUI.ViewModels;
using StudentRecordsUI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StudentRecordsUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //Fetch the view model from Autofac.
        MainPageViewModel ViewModel = App._container.Resolve<MainPageViewModel>();
        
        private void mainNavigationDrawer_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(NavigationMenuItemModel item in mainNavigationDrawer.MenuItems)
            {
                if(item.Content == "Home")
                {
                    mainNavigationDrawer.SelectedItem = item;
                    break;
                }
            }
            //When the nav bar loads, navigate to Home.
            contentFrame.Navigate(typeof(Home));
        }

        //When a nav item is clicked
        private void mainNavigationDrawer_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navItem = args.InvokedItem as NavigationMenuItemModel;
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(Settings));
            }
            if (navItem.Content == "Log Out")
            {
                //If the nav item was logout, log the user out and navigate back to login
                ViewModel.LogOut();
                Frame.Navigate(typeof(Login));
                return;
            }

            //Navigate to that nav item
            contentFrame.Navigate(navItem.ViewType);
            mainNavigationDrawer.Header = navItem.Content;
        }
    }
}
