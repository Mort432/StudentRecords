using Autofac;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Windows.Controls;

namespace StudentRecordsWPFUI.Pages
{
    public partial class LecturerProfile : Page
    {
        public LecturerProfile()
        {
            InitializeComponent();
        }

        private LecturerProfileViewModel ViewModel = App.Container.Resolve<LecturerProfileViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }
    }
}
