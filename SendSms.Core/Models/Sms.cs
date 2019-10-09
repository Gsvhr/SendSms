using System;
using System.Collections.Generic;
using System.Text;

namespace SendSms.Core.Models
{
    public class Sms
    {
        public string sms_id { get; set; }
        public string status { get; set; } 
        public ResponseOnBalanceRequest status_code { get; set; }
        public double cost { get; set; } 
        public int sms { get; set; }        
    }
}
