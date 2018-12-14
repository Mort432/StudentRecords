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
        //Contains the internal frame
        public static Frame WindowFrame { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        //Inject ViewModel from Autofac
        private MainPageViewModel ViewModel = App.Container.Resolve<MainPageViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            WindowFrame = ContentFrame;

            WindowFrame.Navigate(new Login());

            base.OnInitialized(e);
        }
    }
}
