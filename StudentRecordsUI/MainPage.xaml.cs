﻿using Autofac;
using StudentRecordsModels.Models;
using StudentRecordsUI.ViewModels;
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

        MainPageViewModel ViewModel = App._container.Resolve<MainPageViewModel>();

        #region MainNavBar event handlers
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
            contentFrame.Navigate(typeof(Home));
        }

        private void mainNavigationDrawer_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void mainNavigationDrawer_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //TextBlock itemContent = args.InvokedItem as TextBlock;
            var test = args.InvokedItem as NavigationMenuItemModel;
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(Settings));
            }
            //else if (itemContent.Tag != null)
            //{
            //    switch (itemContent.Tag)
            //    {
            //        case "Home_Text":
            //            contentFrame.Navigate(typeof(Home));
            //            mainNavigationDrawer.Header = "Welcome";
            //            break;
            //        case "Students_Text":
            //            contentFrame.Navigate(typeof(Students));
            //            mainNavigationDrawer.Header = "Students";
            //            break;
            //        case "Lecturers_Text":
            //            contentFrame.Navigate(typeof(Lecturers));
            //            mainNavigationDrawer.Header = "Lecturers";
            //            break;
            //        case "Modules_Text":
            //            contentFrame.Navigate(typeof(Modules));
            //            mainNavigationDrawer.Header = "Modules";
            //            break;
            //    }
            //}
            contentFrame.Navigate(test.ViewType);
            mainNavigationDrawer.Header = test.Content;
        }
        #endregion
    }
}
