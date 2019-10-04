using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SendSms.Core.Services
{
    public class HttpDataService
    {
        private HttpClient client;

        public HttpDataService(string defaultBaseUrl = "https://sms.ru")
        {
            client = new HttpClient();

            if (!string.IsNullOrEmpty(defaultBaseUrl))
            {
                client.BaseAddress = new Uri($"{defaultBaseUrl}/");
            }
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            T result = default(T);

            var json = await client.GetStringAsync(uri);
            result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json));

            return result;
        }        
    }
}
