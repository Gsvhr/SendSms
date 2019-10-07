using System;
using System.Threading.Tasks;
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

        public MainViewModel()
        {
        }

        public async Task LoadBalanceAsync() 
        {
            var b = await http.GetAsync<AccountBalance>("my/balance?api_id=12E2FE13-B040-F7FA-D57C-CF42531E9DFE&json=1");
            Balance = "Баланс составляет: " + b.Balance.ToString() + " руб.";
        }
    }
}
