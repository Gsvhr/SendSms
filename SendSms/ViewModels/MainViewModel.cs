using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SendSms.Core.Models;
using SendSms.Core.Services;
using SendSms.Helpers;

namespace SendSms.ViewModels
{
    public class MainViewModel : Observable
    {
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

            set { Set(ref _phone, value); }
        }

        private string _text;
        public string Text 
        {
            get { return _text; }

            set { Set(ref _text, value); }
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
        }

        public async Task LoadBalanceAsync() 
        {
            var b = await http.GetAsync<AccountBalance>("my/balance?api_id=12E2FE13-B040-F7FA-D57C-CF42531E9DFE&json=1");
            Balance = "Баланс составляет: " + b.Balance.ToString() + " руб.";
        }
        public async Task LoadPriceAsync() 
        {
            var b = await http.GetAsync<AccountBalance>("my/balance?api_id=12E2FE13-B040-F7FA-D57C-CF42531E9DFE&json=1");
            Balance = "Баланс составляет: " + b.Balance.ToString() + " руб.";
        }

        public void SendSms ()
        {
            var b = 7;
        }
    }
}
