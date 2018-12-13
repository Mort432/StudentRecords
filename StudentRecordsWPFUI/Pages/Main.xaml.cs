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

        private MainPageViewModel ViewModel = App.Container.Resolve<MainPageViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }

        private void HamburgerMenu_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            var navItem = e.InvokedItem as NavigationMenuItemModel;

            if (navItem.Content == "Log Out")
            {
                ViewModel.LogOut();

                MainWindow.WindowFrame.Navigate(typeof(Login));
            }
            else
            {
                ContentFrame.Navigate(Activator.CreateInstance(navItem.ViewType));
            }
        }
    }
}
