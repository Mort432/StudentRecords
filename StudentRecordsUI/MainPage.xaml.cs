using StudentRecordsUI.Views;
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

        #region MainNavBar event handlers
        private void mainNavigationDrawer_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(NavigationViewItemBase item in mainNavigationDrawer.MenuItems)
            {
                if(item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    mainNavigationDrawer.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(Home));
        }

        private void mainNavigationDrawer_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void mainNavigationDrawer_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock itemContent = args.InvokedItem as TextBlock;
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(Settings));
            }
            else if (itemContent.Tag != null)
            {
                switch (itemContent.Tag)
                {
                    case "Home_Text":
                        contentFrame.Navigate(typeof(Home));
                        mainNavigationDrawer.Header = "Welcome";
                        break;
                    case "Students_Text":
                        contentFrame.Navigate(typeof(Students));
                        mainNavigationDrawer.Header = "Students";
                        break;
                    case "Lecturers_Text":
                        contentFrame.Navigate(typeof(Lecturers));
                        mainNavigationDrawer.Header = "Lecturers";
                        break;
                }
            }
        }
        #endregion
    }
}
