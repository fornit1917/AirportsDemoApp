using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirportsDemo.App.Utils
{
    public class HttpUtils
    {
        public static async Task<TResult> GetJsonAsync<TResult>(HttpClient httpClient, string uri) {
            HttpResponseMessage resp = await httpClient.GetAsync(uri);
            string json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
