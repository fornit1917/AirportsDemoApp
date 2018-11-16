using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AirportsDemo.App.Services;
using AirportsDemo.App.Models;
using Newtonsoft.Json;
using AirportsDemo.App.Utils;

namespace AirportsDemo.App.Services.Impl
{
    public class FlightsApiClient : IFlightsApiClient
    {
        HttpClient httpClient;

        public FlightsApiClient(HttpClient httpClient) {
            this.httpClient = httpClient;
        }

        public async Task<Airline> GetAirlineAsync(string alias) {
            Airline[] airlines = await HttpUtils.GetJsonAsync<Airline[]>(httpClient, $"/api/Airline/{alias}");
            return airlines.Length > 0 ? airlines[0] : null;
        }

        public Task<Flight[]> GetOutgoingFlightsAsync(string airportCode) {
            return HttpUtils.GetJsonAsync<Flight[]>(httpClient, $"/api/Route/outgoing?airport={airportCode}");
        }
    }
}
