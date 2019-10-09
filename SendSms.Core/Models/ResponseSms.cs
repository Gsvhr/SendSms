using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SendSms.Core.Models 
{
    public class ResponseSms
    {
        public string status { get; set; }

        public ResponseOnBalanceRequest status_code { get; set; }

        public Dictionary<string, Sms> sms { get; set; }

        public double balance { get; set; }

        public double total_cost { get; set; }

        public int total_sms { get; set; }
    }
}
