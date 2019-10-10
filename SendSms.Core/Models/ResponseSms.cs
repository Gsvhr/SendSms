using System.Collections.Generic;

namespace SendSms.Core.Models
{
    public class ResponseSms
    {
        public string status { get; set; }

        public ResponseOnRequest status_code { get; set; }

        public Dictionary<string, Message> sms { get; set; }

        public double balance { get; set; }

        public double total_cost { get; set; }

        public int total_sms { get; set; }
    }
}
