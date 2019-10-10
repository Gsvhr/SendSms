using Microsoft.EntityFrameworkCore;
using System.Linq;
using SendSms.EntityFramework;
using SendSms.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SendSms.Views
{
    public sealed partial class TemplatePage : Page
    {
        public TemplateViewModel ViewModel { get; } = new TemplateViewModel();

        public TemplatePage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await ViewModel.LoadDataAsync();
        }

        private void DataGrid_CellEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridCellEditEndedEventArgs e)
        {
            var t = ViewModel.Selected;

            using (var db = new SendSmsContext())
            {
                var n = db.Templates.FirstOrDefault(x => x.Id == t.Id);
                if (n == null)
                {
                    db.Add(t);
                }
                else
                {
                    db.Entry(n).CurrentValues.SetValues(t);
                }
                db.SaveChanges();
            }
        }
    }
}
