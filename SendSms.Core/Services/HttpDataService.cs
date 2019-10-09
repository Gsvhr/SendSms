using SendSms.Core.Helpers;
using SendSms.Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
