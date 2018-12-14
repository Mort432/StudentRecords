using Autofac;
using StudentRecordsViewModels.ViewModels;
using System;
using System.Windows.Controls;

namespace StudentRecordsWPFUI.Pages
{
    public partial class StudentProfile : Page
    {
        public StudentProfile()
        {
            InitializeComponent();
        }

        //Inject ViewModel from Autofac
        private StudentProfileViewModel ViewModel = App.Container.Resolve<StudentProfileViewModel>();

        protected override void OnInitialized(EventArgs e)
        {
            DataContext = ViewModel;

            base.OnInitialized(e);
        }
    }
}
