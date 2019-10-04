using System;
using System.Threading.Tasks;
using SendSms.Core.Services;
using SendSms.Helpers;

namespace SendSms.ViewModels
{
    public class MainViewModel : Observable
    {
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
            HttpDataService.
            Balance = "100";
        }
    }
}
