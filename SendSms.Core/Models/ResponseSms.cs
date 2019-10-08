using System.Text.Json.Serialization;

namespace SendSms.Core.Models 
{
    public class ResponseSms
    {
        public string Status { get; set; }
        public ResponseOnBalanceRequest Status_code { get; set; }
        public double Balance { get; set; }
        public double Total_cost { get; set; }
        public int Total_sms { get; set; }
    }
}
