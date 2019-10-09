using SendSms.Core.Models;
using SendSms.Core.Services;
using SendSms.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SendSms.ViewModels
{
    public class ArchiveViewModel : Observable
    {
        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public ArchiveViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // TODO WTS: Replace this with your actual data
            var data = await SampleDataService.GetGridDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
