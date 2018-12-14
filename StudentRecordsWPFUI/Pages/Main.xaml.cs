using Autofac;
using MahApps.Metro.Controls;
using StudentRecordsModels.Models;
using StudentRecordsWPFUI.ViewModels;
using System;
using System.Windows.Controls;

namespace StudentRecordsWPFUI.Pages
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        //Inject ViewModel from Autofac
        private MainPageViewModel ViewModel = App.Container.Resolve<MainPageViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }

        //If a nav menu item is selected
        private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            //Get the nav item
            var navItem = e.InvokedItem as NavigationMenuItemModel;

            if (navItem.Content == "Log Out")
            {
                //If it was the logout option...
                ViewModel.LogOut();

                MainWindow.WindowFrame.Navigate(new Login());
            }
            else
            {
                //Navigate to that page
                ContentFrame.Navigate(Activator.CreateInstance(navItem.ViewType));
            }
        }
    }
}
