using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AirportsDemo.App.Services;
using AirportsDemo.App.Models;
using Newtonsoft.Json;

namespace AirportsDemo.App.Services.Impl
{
    public class FlightsApiClient : IFlightsApiClient
    {
        HttpClient httpClient;

        public FlightsApiClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<Airline> GetAirlineAsync(string alias) {
            Airline[] airlines = await GetJsonAsync<Airline[]>($"/api/Airline/{alias}");
            return airlines.Length > 0 ? airlines[0] : null;
        }

        public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode) {
            return GetJsonAsync<Flight[]>($"/api/Route/outgoing?airport={airportCode}");
        }

        private async Task<TResult> GetJsonAsync<TResult>(string uri) {
            HttpResponseMessage resp = await httpClient.GetAsync(uri);
            resp.EnsureSuccessStatusCode();
            string json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
