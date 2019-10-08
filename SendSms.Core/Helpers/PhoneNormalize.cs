using System;

namespace SendSms.Core.Helpers
{
    public static class PhoneNormalize
    {
        public static string GetPhoneString(string text)
        {
            string phonesNormalize = "";
            char[] chars = { ',' };
            string[] phones = text.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            foreach (var phone in phones)
            {
                if (phone.Length > 9)
                {
                    var p = "7" + phone.Substring(phone.Length - 10);
                    if (phonesNormalize.Length > 0)
                    {
                        phonesNormalize += ",";
                    }
                    phonesNormalize += p;
                }
            }
            return phonesNormalize;
        }
    }
}
