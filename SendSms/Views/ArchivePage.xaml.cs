using System;

using SendSms.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SendSms.Views
{
    public sealed partial class ArchivePage : Page
    {
        public ArchiveViewModel ViewModel { get; } = new ArchiveViewModel();

        // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on ArchivePage.xaml.
        // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
        public ArchivePage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }
    }
}
