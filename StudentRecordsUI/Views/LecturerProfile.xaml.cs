using StudentRecordsViewModels.ViewModels;
using Windows.UI.Xaml.Controls;
using Autofac;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StudentRecordsUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LecturerProfile : Page
    {
        public LecturerProfile()
        {
            this.InitializeComponent();
        }

        //Fetch the view model from Autofac.
        public LecturerProfileViewModel viewModel = App._container.Resolve<LecturerProfileViewModel>();
    }
}
