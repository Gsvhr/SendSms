using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SendSms.Core.Helpers;
using SendSms.Core.Models;
using SendSms.Core.Services;
using SendSms.Helpers;
using Windows.Storage;

namespace SendSms.ViewModels
{
    public class MainViewModel : Observable
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private string ApiId; 

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

            set { Set(ref _phone, value); if (value != "") LoadTotalCostAsync(); CanSend = Phone.Length > 9; }
        }

        private string _text;
        public string Text 
        {
            get { return _text; }

            set { Set(ref _text, value); if (value != "") LoadTotalCostAsync(); }
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
            ApiId = localSettings.Values["APIID"].ToString();
        }

        public async Task LoadBalanceAsync() 
        {
            var b = await http.GetAsync<ResponseSms>($"my/balance?api_id={ApiId}&json=1");
            Balance = "Баланс составляет: " + b.Balance.ToString() + " руб.";
        }

        public async void LoadTotalCostAsync()  
        {
            if (CanSend)
            {
                var p = PhoneNormalize.GetPhoneString(Phone);
                var c = await http.GetAsync<ResponseSms>($"sms/cost?api_id={ApiId}&to={p}&msg={Text}&json=1");
                TotalCost = "Сообщений: " + c.Total_sms.ToString() + "     Общая стоимость: " + c.Total_cost.ToString() + " руб.";
            }
        }

        public async void SendSms ()
        {
            var p = PhoneNormalize.GetPhoneString(Phone);
            var b = await http.GetAsync<ResponseSms>($"sms/send?api_id={ApiId}&to={p}&msg={Text}&json=1");
            if (b.Status == "OK")
            {
                Text = Phone = "";                
                Balance = "Баланс составляет: " + b.Balance.ToString() + " руб.";
                TotalCost = "Сообщения отправлены!";
            }
            else
            {
                TotalCost = "Ошибка отправки сообщений.";
            }
        }
    }
}
