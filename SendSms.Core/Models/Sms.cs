namespace SendSms.Core.Models
{
    public class Sms
    {
        public int sms_id { get; set; }
        public string status { get; set; }
        public ResponseOnRequest status_code { get; set; }
        public double cost { get; set; }
        public int sms { get; set; }
    }
}
