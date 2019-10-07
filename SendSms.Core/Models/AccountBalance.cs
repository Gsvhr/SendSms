namespace SendSms.Core.Models 
{
    public class AccountBalance 
    {
        public string Status { get; set; }
        public ResponseOnBalanceRequest Status_code { get; set; }
        public double Balance { get; set; }
    }
}
