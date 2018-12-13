using Autofac;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Windows.Controls;

namespace StudentRecordsWPFUI.Pages
{
    public partial class LecturerStudentAnalytics : Page
    {
        public LecturerStudentAnalytics()
        {
            InitializeComponent();
        }

        private LecturerStudentAnalyticsViewModel ViewModel = App.Container.Resolve<LecturerStudentAnalyticsViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }
    }
}
