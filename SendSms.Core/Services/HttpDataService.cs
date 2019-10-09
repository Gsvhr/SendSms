using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using SendSms.Core.Helpers;
using SendSms.Core.Models;

namespace SendSms.Core.Services
{
    public class HttpDataService
    {
        private readonly HttpClient client;

        public HttpDataService(string defaultBaseUrl = "https://sms.ru")
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(defaultBaseUrl))
            {
                client.BaseAddress = new Uri($"{defaultBaseUrl}/");
            }
        }

        public async Task<ResponseSms> GetAsync(string uri)
        {
            var json = await client.GetStringAsync(uri);
            return await Json.ToObjectAsync<ResponseSms>(json);
        }        
    }
}
