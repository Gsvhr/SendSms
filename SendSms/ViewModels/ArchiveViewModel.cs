using Microsoft.EntityFrameworkCore;
using SendSms.Core.Models;
using SendSms.Core.Services;
using SendSms.EntityFramework;
using SendSms.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace SendSms.ViewModels
{
    public class ArchiveViewModel : Observable
    {
        public ObservableCollection<Message> Source { get; } = new ObservableCollection<Message>();
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private readonly HttpDataService http = new HttpDataService();
        private readonly string ApiId;
        public ArchiveViewModel()
        {
            if (localSettings.Values.Keys.Contains("APIID"))
            {
                ApiId = localSettings.Values["APIID"].ToString();
            }
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            using (var db = new SendSmsContext())
            {
                var ids = "";
                var sends = await db.Messages.AsNoTracking().Where(x => (x.status_code == ResponseOnRequest.Отправлено || x.status_code == ResponseOnRequest.Сообщение_отправлено)).ToListAsync();

                if (sends?.Count >0)
                {
                    foreach (var item in sends)
                    {
                        ids += $"{item.sms_id},";
                    }
                }

                Dictionary<string, Message> d = null;

                if (ids.Length>1)
                {
                    ids = ids.Remove(ids.Length-1);
                    var uri = $"sms/status?api_id={ApiId}&sms_id={ids}&json=1";
                    d = (await http.GetAsync(uri))?.sms;
                }               

                if ( d?.Count>0)
                {
                    foreach (var item in d)
                    {
                        var s = db.Messages.SingleOrDefault(x => x.sms_id == item.Key);
                        s.status_code = item.Value.status_code;
                        db.SaveChanges();
                    }
                }
                
                var data = await db.Messages.AsNoTracking().OrderByDescending(x => x.time).ToListAsync();
                foreach (var item in data)
                {
                    Source.Add(item);                    
                }
            }
        }
    }
}
