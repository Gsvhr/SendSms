using System;
using System.ComponentModel.DataAnnotations;

namespace SendSms.Core.Models
{
    public class Message
    {
        [Key]
        public string sms_id { get; set; }
        public string status { get; set; }
        public ResponseOnRequest status_code { get; set; }
        public string content { get; set; }
        public int sms { get; set; }
        public DateTime time { get; set; }
    }
}
