using SendSms.Core.Helpers;
using SendSms.Core.Services;
using SendSms.EntityFramework;
using SendSms.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace SendSms.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private readonly string ApiId;

        private readonly HttpDataService http = new HttpDataService();
        private string _balance;
        public string Balance
        {
            get { return _balance; }

            set { Set(ref _balance, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }

            set { Set(ref _phone, value); CanSend = Phone.Length > 9; if (value != "") LoadCostAsync(); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }

            set { Set(ref _text, value); if (value != "") LoadCostAsync(); }
        }

        private string _totalCost;
        public string TotalCost
        {
            get { return _totalCost; }

            set { Set(ref _totalCost, value); }
        }

        private bool _canSend;
        public bool CanSend
        {
            get { return _canSend; }

            set { Set(ref _canSend, value); }
        }

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(SendSms);
                }

                return _sendCommand;
            }
        }

        public MainViewModel()
        {
            Phone = "";
            if (localSettings.Values.Keys.Contains("APIID"))
            {
                ApiId = localSettings.Values["APIID"].ToString();
            }
        }

        public async Task InitAsync()
        {
            await LoadBalanceAsync();
        }

        public async Task LoadBalanceAsync()
        {
            var uri = $"my/balance?api_id={ApiId}&json=1";
            var b = await http.GetAsync(uri);
            if (b.status == "OK")
            {
                Balance = "Баланс составляет: " + b.balance.ToString() + " руб.";
            }
            else
            {
                Balance = b.status_code.ToString();
            }
        }

        public async void LoadCostAsync()
        {
            if (CanSend)
            {
                var p = PhoneNormalize.GetPhoneString(Phone);
                var uri = $"sms/cost?api_id={ApiId}&to={p}&msg={Text}&json=1";
                var c = await http.GetAsync(uri);
                if (c.status == "OK")
                {
                    foreach (var item in c.sms)
                    {
                        var a = item.Key;
                        var b = item.Value;
                    }
                    TotalCost = "Сообщений: " + c.total_sms.ToString() + "     Общая стоимость: " + c.total_cost.ToString() + " руб.";
                }
            }
            else
            {
                TotalCost = "";
            }
        }

        public async void SendSms()
        {
            var p = PhoneNormalize.GetPhoneString(Phone);
            var b = await http.GetAsync($"sms/send?api_id={ApiId}&to={p}&msg={Text}&json=1&test=1");
            if (b.status == "OK")
            {
                TotalCost = "";
                foreach (var item in b.sms)
                {
                    var phone = item.Key.ToString();
                    TotalCost += phone + " : " + item.Value.status_code + ", ";
                    item.Value.phone = phone;
                    item.Value.content = Text;
                    item.Value.time = DateTime.Now;
                    using (var db = new SendSmsContext())
                    {
                        db.Add(item.Value);
                        db.SaveChanges();
                    }
                }
                Text = Phone = "";
                Balance = "Баланс составляет: " + b.balance.ToString() + " руб.";                
            }
            else
            {
                TotalCost = b.status_code.ToString();
            }            
        }
    }
}
