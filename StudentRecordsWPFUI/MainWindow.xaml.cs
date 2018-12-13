using Autofac;
using MahApps.Metro.Controls;
using StudentRecordsWPFUI.Pages;
using StudentRecordsWPFUI.ViewModels;
using System;
using System.Windows.Controls;

namespace StudentRecordsWPFUI
{
    public partial class MainWindow : MetroWindow
    {
        public static Frame WindowFrame { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private MainPageViewModel ViewModel = App.Container.Resolve<MainPageViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            WindowFrame = ContentFrame;

            WindowFrame.Navigate(new Login());

            base.OnInitialized(e);
        }
    }
}
